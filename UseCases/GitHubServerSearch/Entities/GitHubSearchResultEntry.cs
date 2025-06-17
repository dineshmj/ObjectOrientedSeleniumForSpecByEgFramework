using System.Collections.ObjectModel;

namespace GitHubServerSearch.Entities
{
	public sealed class GitHubSearchResultEntry
	{
		private readonly IList<MatchingStatement> statements;

		public IList<MatchingStatement> MatchingStatements => new ReadOnlyCollection<MatchingStatement> (this.statements);

		public string ProjectName { get; init; }

		public string FileName { get; init; }

		public string Extension { get; init; }

		public GitHubSearchResultEntry (string projectName, string fileName, IList<MatchingStatement> lines, string extension)
		{
			this.ProjectName = projectName;
			this.FileName = fileName;
			this.statements = lines;
			this.Extension = extension;
		}
	}
}