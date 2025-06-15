using System.Drawing;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;
using System.Text.RegularExpressions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class Image
		: WebUiControlBase
	{
		public Image (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (element, uniqueIdentifierText, byWhat, webDriver)
		{
			if (element.TagName.ToLower () != "img")
			{
				throw new ArgumentException ("Element is not a picture", nameof (element));
			}

			var javaScriptPart = string.Empty;
			switch (base.uniqueIdentifierType)
			{
				case LocateByWhat.Id:
					var id1 = base.uniqueIdentifierText;
					javaScriptPart
						= @"var image = document.getElementById ('" + id1 + @"');;";
					break;

				case LocateByWhat.XPath:
					var xPath = base.uniqueIdentifierText;
					javaScriptPart
						= @"var xpathResult = document.evaluate(
								" + Regex.Escape (xPath) + @",
								document, // where to start search
								null,     // No namespace resolver
								XPathResult.FIRST_ORDERED_NODE_TYPE, // Return the first matching element
								null      // Result object to re-use (null for a new one)
							);

							var image = xpathResult.singleNodeValue;";
					break;
				default:
					throw new NotSupportedException ($"ByWhat type `{base.uniqueIdentifierType}` is not supported for Image control.");
			}

			var base64string
				= base.webDriver.ExecuteJavaScript<string>
					(
						@"var imageCanvas = document.createElement('canvas');
						var context = imageCanvas.getContext('2d');
						" + javaScriptPart + @"

						imageCanvas.height = image.naturalHeight;
						imageCanvas.width = image.naturalWidth;

						context.drawImage(image, 0, 0, image.naturalWidth, image.naturalHeight);
						var base64String = imageCanvas.toDataURL();
						return base64String;"
					);

			this.Base64String = base64string.Split (',').Last ();
			using (var stream = new MemoryStream (Convert.FromBase64String (this.Base64String)))
			{
				this.ImageBitmap = new Bitmap (stream);
			}
		}
	
		~Image ()
		{
			this.ImageBitmap?.Dispose ();
		}

		public string SourceUrl
		{
			get { return base.GetAttribute ("src"); }
		}

		public Bitmap ImageBitmap { get; private set; }

		public string Base64String { get; private set; }
	}
}