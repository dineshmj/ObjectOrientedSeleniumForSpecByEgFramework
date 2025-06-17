using System.Runtime.InteropServices;

public static class DpiHelper
{
	[DllImport ("user32.dll")]
	private static extern IntPtr MonitorFromWindow (IntPtr hwnd, uint dwFlags);

	[DllImport ("Shcore.dll")]
	private static extern int GetDpiForMonitor (
		IntPtr hmonitor,
		MonitorDpiType dpiType,
		out uint dpiX,
		out uint dpiY);

	private const uint MONITOR_DEFAULTTONEAREST = 2;

	private enum MonitorDpiType
	{
		MDT_EFFECTIVE_DPI = 0,
		MDT_ANGULAR_DPI = 1,
		MDT_RAW_DPI = 2,
		MDT_DEFAULT = MDT_EFFECTIVE_DPI
	}

	public static float GetScalingFactor (Form form)
	{
		IntPtr hMonitor = MonitorFromWindow (form.Handle, MONITOR_DEFAULTTONEAREST);

		int result = GetDpiForMonitor (hMonitor, MonitorDpiType.MDT_EFFECTIVE_DPI, out uint dpiX, out uint dpiY);

		if (result != 0)
		{
			// Default to 96 DPI = 100%
			return 1.0f;
		}

		return dpiX / 96.0f; // 96 DPI is 100%
	}
}