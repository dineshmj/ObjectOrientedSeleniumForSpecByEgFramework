using System;
using System.Collections.Generic;

using OpenQA.Selenium;

namespace OOSelenium.Framework.Extensions
{
	public static class WebElementExtensions
	{
		public static string Value (this IWebElement webElement)
		{
			var elementValue = webElement.GetProperty ("value");
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

		public static void ClickLink (this IWebElement anchorTag, IWebDriver webDriver)
		{
			var jsEngine = (IJavaScriptExecutor) webDriver;
			jsEngine.ExecuteScript ($"arguments [0].click ();", anchorTag);
		}

		public static string GetInnerText (this IWebElement spanTag, IWebDriver webDriver, string id)
		{
			int attempts = 0;

			while (attempts < 10)
			{
				var jsEngine = (IJavaScriptExecutor) webDriver;

				try
				{
					var innerText = jsEngine.ExecuteScript ("return arguments [0].innerHTML;", spanTag).ToString ();
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
