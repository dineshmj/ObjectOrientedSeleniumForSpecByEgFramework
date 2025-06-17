namespace GitHubServerSearch.Entities
{
	public sealed class MatchingStatement
	{
		public string LineNumber { get; init; }

		public string Text { get; init; }

		public MatchingStatement (string lineNumber, string lineText)
		{
			this.LineNumber = lineNumber;
			this.Text = lineText;
		}
	}
}