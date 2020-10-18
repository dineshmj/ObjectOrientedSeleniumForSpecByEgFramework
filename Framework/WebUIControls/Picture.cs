using System;
using System.Drawing;
using System.IO;
using System.Linq;

using OOSelenium.Framework.Abstractions;

using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class Picture
		: WebUiControlBase
	{
		public Picture (RemoteWebElement element, string id)
			: base (element, id)
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