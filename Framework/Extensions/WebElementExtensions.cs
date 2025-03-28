using OpenQA.Selenium;

namespace OOSelenium.Framework.Extensions
{
	public static class WebElementExtensions
	{
		public static string Value (this IWebElement webElement)
		{
			var elementValue = webElement.GetDomProperty ("value");
			var elementText = webElement.Text;

			return
				String.IsNullOrEmpty (elementValue) || String.IsNullOrWhiteSpace (elementValue)
					? elementText
					: elementValue;
		}

		public static void SetValue (this IWebElement webElement, string valueText, IWebDriver webDriver)
		{
			var jsEngine = (IJavaScriptExecutor) webDriver;
			jsEngine.ExecuteScript ($"arguments [0].setAttribute ('value', '{ valueText }');", webElement);
		}

		public static void SetFocus (this IWebElement webElement, IWebDriver webDriver)
		{
			var jsEngine = (IJavaScriptExecutor) webDriver;
			jsEngine.ExecuteScript ("arguments [0].focus ();", webElement);
		}

		public static void ClickLink (this IWebElement anchorTag, IWebDriver webDriver)
		{
			var jsEngine = (IJavaScriptExecutor) webDriver;
			jsEngine.ExecuteScript ($"arguments [0].click ();", anchorTag);
		}

		public static bool IsAnchorClickable (this IWebElement anchorElement)
		{
			var href = anchorElement.GetAttribute ("href");

			if (string.IsNullOrEmpty (href) || string.IsNullOrWhiteSpace (href))
			{
				return false;
			}

			var classAttribute = anchorElement.GetAttribute ("class");

			if (anchorElement.GetAttribute ("disabled") != null
				|| (
					classAttribute != null
					&& classAttribute.Contains ("disabled")
				))
			{
				return false;
			}

			if (! anchorElement.Displayed || !anchorElement.Enabled)
			{
				return false;
			}

			return true;
		}

		public static string GetInnerText (this IWebElement element, IWebDriver webDriver)
		{
			var text = element.Text;
			var jsEngine = (IJavaScriptExecutor) webDriver;

			var innerText = (string) jsEngine.ExecuteScript (
				"return Array.from(arguments[0].childNodes).filter(node => node.nodeType === Node.TXT_NODE).map(node => node.textContent).join('');",
				element);

			return string.IsNullOrEmpty (innerText) || string.IsNullOrWhiteSpace (innerText)
				? text
				: innerText;
		}

		public static string GetInnerText (this IWebElement tagWithText, IWebDriver webDriver, string id)
		{
			int attempts = 0;

			while (attempts < 10)
			{
				var jsEngine = (IJavaScriptExecutor) webDriver;

				try
				{
					// If it is an input field (text box, radio button, etc.), then go one level up to its parent
					// <span>, <div> or <label> and get its text.
					if (tagWithText.TagName == "input")
					{
						tagWithText = tagWithText.FindElement (By.XPath (".."));
						return tagWithText.Text;
					}

					var innerText = jsEngine.ExecuteScript ("return arguments [0].innerHTML;", tagWithText).ToString ();
					return innerText;
				}
				catch (StaleElementReferenceException se1)
				{
					try
					{
						var element = webDriver.FindElement (By.Id (id));
						var innerText = element.Text;

						return innerText;
					}
					catch (StaleElementReferenceException se2)
					{
					}
				}

				attempts++;
			}

			throw new StaleElementReferenceException ($"Cannot locate an element with id \"{ id }\".");
		}

		public static string GetOuterHTML (this IWebElement element)
		{
			return element.GetAttribute ("outerHTML");
		}

		public static IList<string> ReadBulletEntries (this IWebElement divTag, IWebDriver webDriver, string id)
		{
			var bulletEntries = new List<string> ();

			var ulElement = divTag.FindElement (By.XPath ("./ul"));

			if (ulElement != null)
			{
				var liElements = ulElement.FindElements (By.XPath ("./li"));

				foreach (var oneLiElement in liElements)
				{
					bulletEntries.Add (oneLiElement.Text);
				}
			}

			return bulletEntries;
		}
	}
}