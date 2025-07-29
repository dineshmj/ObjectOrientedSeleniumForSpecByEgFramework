namespace SimpleQA.Tools.WebUIPageStudio
{
	internal static class Program
	{
		[STAThread]
		static void Main ()
		{
			ApplicationConfiguration.Initialize ();
			Application.Run (new WebUIPageStudioScreen ());
		}
	}
}