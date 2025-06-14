namespace OOSelenium.WebUIPageStudio.Helpers
{
	public static class StringExtensions
	{
		public static string FormPascalCaseNameFromDescription (this string? description)
		{
			if (string.IsNullOrWhiteSpace (description))
			{
				return string.Empty;
				// throw new ArgumentException ("Description cannot be null or empty.", nameof (description));
			}

			// Remove non-alphanumeric characters except for spaces, hyphens, and underscores.
			description = new string (description.Where (c => char.IsLetterOrDigit (c) || c == ' ' || c == '-' || c == '_').ToArray ());

			var words = description.Split ([ ' ', '-', '_' ], StringSplitOptions.RemoveEmptyEntries);

			var pascalCaseName = string.Concat (words.Select (word => char.ToUpperInvariant (word [0]) + word.Substring (1).ToLowerInvariant ()));
			return pascalCaseName;
		}
	}
}