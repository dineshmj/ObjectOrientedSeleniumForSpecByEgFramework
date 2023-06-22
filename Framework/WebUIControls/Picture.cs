using System.Drawing;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

using OOSelenium.Framework.Abstractions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class Picture
		: WebUiControlBase
	{
		public Picture (IWebElement element, string id, IWebDriver webDriver)
			: base (element, id, webDriver)
		{
			var base64string
				= base.webDriver.ExecuteJavaScript<string>
					(
						@"var imageCanvas = document.createElement('canvas');
						var context = imageCanvas.getContext('2d');
						var image = document.getElementById('" + base.id + @"');

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
	
		~Picture ()
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