namespace OOSelenium.Framework.Extensions
{
	public static class StringExtensions
	{
		public static bool IsNullEmptyOrWhitespace (this string text)
		{
			return string.IsNullOrEmpty (text) || string.IsNullOrWhiteSpace (text);
		}

		public static bool IsNotNullEmptyOrWhitespace (this string text)
		{
			return !string.IsNullOrEmpty (text) && !string.IsNullOrWhiteSpace (text);
		}

		public static bool AllOfThemPresent (this string source, params string [] subStrings)
		{
			foreach (var oneSubString in subStrings)
			{
				if (source.Contains (oneSubString) == false)
				{
					return false;
				}
			}

			return true;
		}

		public static bool AnyOfThemPresent (this string source, params string [] subStrings)
		{
			foreach (var oneSubString in subStrings)
			{
				if (source.Contains (oneSubString))
				{
					return true;
				}
			}

			return false;
		}
	}
}