using OOSelenium.WebUIPageStudio.Entities;

namespace OOSelenium.WebUIPageStudio
{
	public partial class WebPageModelDetailsScreen
		: Form
	{
		private static WebPageModelDetailsScreen? instance;
		private static object syncLocker = new ();

		public static WebPageModelDetailsScreen DefinedInstance
		{
			get
			{
				lock (syncLocker)
				{
					if (instance == null || instance.IsDisposed)
					{
						instance = new WebPageModelDetailsScreen ();
					}
				}
				return instance;
			}
		}

		private WebPageModelDetailsScreen ()
		{
			InitializeComponent ();
		}

		public void LoadSelectedElements (IEnumerable<HtmlTagInfo> htmlTagInfos)
		{
			if (htmlTagInfos == null || !htmlTagInfos.Any ())
			{
				throw new ArgumentNullException (nameof (htmlTagInfos), "The collection of HTML Tag Info instances cannot be null or empty.");
			}

			this.flowLayoutPanel1.Controls.Clear ();

			int index = 1;
			int total = htmlTagInfos.Count ();

			foreach (var htmlTagInfo in htmlTagInfos)
			{
				var control = new UIControlHtmlTagMapperControl ();
				control.MapHtmlTagInfo (htmlTagInfo, index, total);

				this.flowLayoutPanel1.Controls.Add (control);
				index++;
			}
		}
	}
}