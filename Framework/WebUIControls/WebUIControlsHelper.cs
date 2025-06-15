using System.Collections.ObjectModel;

using OOSelenium.Framework.Abstractions;

namespace OOSelenium.Framework.WebUIControls
{
	public static class WebUIControlsHelper
	{
		public static ReadOnlyCollection<Type> GetSupportedWebUiControls ()
		{
			var webUiControlTypes
				= typeof (WebUiControlBase)
					.Assembly
					.GetTypes ()
					.Where (t => t.IsSubclassOf (typeof (WebUiControlBase)) && !t.IsAbstract)
					.ToList ();
			
			return
				new ReadOnlyCollection<Type> (webUiControlTypes);
		}
	}
}