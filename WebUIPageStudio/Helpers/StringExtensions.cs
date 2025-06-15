using System.Text.RegularExpressions;

namespace OOSelenium.WebUIPageStudio.Helpers
{
	public static class StringExtensions
	{
		public static string StripNonAlphaPrefix (this string text)
		{
			if (string.IsNullOrEmpty (text))
			{
				return text;
			}

			var strippedText = Regex.Replace (text, @"^[^a-zA-Z]*", "", RegexOptions.CultureInvariant).Trim ();

			if (string.IsNullOrEmpty (strippedText))
			{
				return string.Empty;
			}

			if (strippedText.Length == 1)
			{
				return $"{strippedText [0].ToString ().ToUpper ()}";
			}

			return $"{strippedText [0].ToString ().ToUpper ()}{strippedText.Substring (1)}";
		}

		public static string FormPascalCaseNameFromDescription (this string? description)
		{
			if (string.IsNullOrWhiteSpace (description))
			{
				throw new ArgumentException ("Description cannot be null or empty.", nameof (description));
			}

			// Remove non-alphanumeric characters except for spaces, hyphens, and underscores.
			description = new string (description.Where (c => char.IsLetterOrDigit (c) || c == ' ' || c == '-' || c == '_').ToArray ());

			var words = description.Split ([ ' ', '-', '_' ], StringSplitOptions.RemoveEmptyEntries);

			var pascalCaseName = string.Concat (words.Select (word => char.ToUpperInvariant (word [0]) + word.Substring (1).ToLowerInvariant ()));
			return pascalCaseName;
		}

		public static string GetAndJoinFirstTwoWords (this string sentence)
		{
			// Handle null or empty input gracefully
			if (string.IsNullOrWhiteSpace (sentence))
			{
				return string.Empty;
			}

			// Use Regex.Matches to find all sequences of one or more English alphabetic characters.
			var matches = Regex.Matches (sentence, "[a-zA-Z]+");

			// Check how many words were found
			if (matches.Count < 2)
			{
				// If only one word is found, PascalCase it and return
				if (matches.Count == 1)
				{
					return matches [0].Value.FormPascalCaseNameFromDescription ();
				}
				// If no words are found, return an empty string
				return string.Empty;
			}

			// Extract the first and second matched words
			string firstWord = matches [0].Value.FormPascalCaseNameFromDescription ();
			string secondWord = matches [1].Value.FormPascalCaseNameFromDescription ();

			// Join the two PascalCased words and return the result
			return $"{firstWord}{secondWord}";
		}
	}
}