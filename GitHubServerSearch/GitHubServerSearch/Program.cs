using GitHubServerSearch.Background;
using GitHubServerSearch.Navigation;

Console.WriteLine ("==============================================================================");
Console.WriteLine ("==                                                                          ==");
Console.WriteLine ("==                  Welcome to GitHub Server Search                         ==");
Console.WriteLine ("==                                                                          ==");
Console.WriteLine ("==============================================================================");
Console.WriteLine ("");

Console.WriteLine ("Enter the GitHub username: ");
var username = Console.ReadLine ();

Console.WriteLine ("Enter the GitHub password: ");
var password = Console.ReadLine ();

Console.WriteLine ();

//
// OOSF framework usage - preparatory steps.
//

var gitHubPageDataProvider = new GitHubPageDataProvider ();
var decorator = new PassThroughDecrypter ();

//
// Use the navigation component, go to the screens, and get your work done.
//
using (var gitHubNavigationComponent = new GitHubNavigationComponent<UserRole, ExecutionEnvironment> (gitHubPageDataProvider, decorator))
using (var gitHubHomePage = gitHubNavigationComponent.LoginConfirmSsoAndGoToGitHubHomePage (username, password))
{
	var searchTerms 
		= new [] { "storedProcedureNameOne", "storedProcedureNameTwo", /* Add more, or read from a file if required */ };

	var searchResultsOutputFile = "GitHubSearchResults.txt";

	if (File.Exists (searchResultsOutputFile))
	{
		File.Delete (searchResultsOutputFile);
	}

	foreach (var oneSearchTerm in searchTerms)
	{
		gitHubHomePage.SearchGitHub (oneSearchTerm);

		foreach (var oneSearchResult in gitHubHomePage.SearchResults)
		{
			File.AppendAllText (searchResultsOutputFile,$"{oneSearchTerm}\t{oneSearchResult.ProjectName}\t{oneSearchResult.FileName}\t{oneSearchResult.Extension}");

			var counter = 1;

			foreach (var oneStatement in oneSearchResult.MatchingStatements)
			{
				if (oneStatement.Text.Contains (oneSearchTerm) == false)
				{
					continue;
				}

				if (counter == 1)
				{
					File.AppendAllText (searchResultsOutputFile, $"\t{oneStatement.LineNumber}\t{oneStatement.Text}{Environment.NewLine}");
				}
				else
				{
					File.AppendAllText (searchResultsOutputFile, $"\t\t\t\t{oneStatement.LineNumber}\t{oneStatement.Text}{Environment.NewLine}");
				}

				counter++;
			}

			if (counter == 1)
			{
				File.AppendAllText (searchResultsOutputFile, Environment.NewLine);
			}
		}
	}
}

Console.WriteLine ("GitHub search is complete. The Search results are saved to the file: GitHubSearchResults.txt.");
Console.WriteLine ("Press any key to exit.");