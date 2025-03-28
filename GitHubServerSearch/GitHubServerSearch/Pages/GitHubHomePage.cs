using System.Collections.ObjectModel;

using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Extensions;
using OOSelenium.Framework.WebUIControls;

using GitHubServerSearch.Background;
using GitHubServerSearch.Entities;

namespace GitHubServerSearch.Pages
{
	public sealed class GitHubHomePage
		: WebUiPageBase
	{
		private readonly List<GitHubSearchResultEntry> searchResults = new List<GitHubSearchResultEntry> ();
		private IList<string> confineExtensions;
		private bool keywordSearchSpanClicked;

		public Link CodeLinkOnLeftPane { get; private set; }

		public Span KeywordSearchSpan { get; private set; }

		public TextField KeywordSearchTextField { get; private set; }

		public IList<GitHubSearchResultEntry> SearchResults => new ReadOnlyCollection<GitHubSearchResultEntry> (this.searchResults);

		public GitHubHomePage (IWebDriver webDriver, string baseUrl)
			: base (webDriver, baseUrl)
		{
			Thread.Sleep (3000);
			this.KeywordSearchSpan = base.FindSpanById (ElementIds.ID_HOME_PAGE_KEYWORD_SEARCH_SPAN);
			this.KeywordSearchTextField = this.FindTextFieldById (ElementIds.ID_HOME_PAGE_KEYWORD_SEARCH_FIELD);
		}

		public void ConfineSearchToExtensions (IList<string> extensions)
		{
			var invalidExtensionsPresent = extensions.Any (ext => ext.Trim ().StartsWith (".") == false);

			if (invalidExtensionsPresent)
			{
				throw new ArgumentException ("Extensions must start with a period (.)");
			}

			this.confineExtensions
				= extensions
					.Select (ext => ext.Trim ())
					.ToList ();
		}

		public void SearchGitHub (string searchKeyword)
		{
			// Locate the "Search" text box and enter the search keyword.
			if (this.keywordSearchSpanClicked == false)
			{
				this.KeywordSearchSpan.Click ();
				this.keywordSearchSpanClicked = true;
			}
			else
			{
				// A search has already taken place once. Since the page has refreshed, we need to re-locate the "Search" text box.
				var divElement = base.GetElementByCss (CssClasses.CSS_SEARCH_TEXT_BOX_DIV.RefineForDiv ());
				divElement.Click ();

				this.KeywordSearchTextField = this.FindTextFieldById (ElementIds.ID_HOME_PAGE_KEYWORD_SEARCH_FIELD);
				this.KeywordSearchTextField.SetFocus ();
			}

			// Clear the text box before typing the search keyword.
			this.KeywordSearchTextField.Clear ();
			this.KeywordSearchTextField.TypeEachCharacter (searchKeyword);
			this.KeywordSearchTextField.SendKeys (Keys.Enter);

			Thread.Sleep (5000);

			// Click on the "Code" link on the left pane to get the search results.
			this.CodeLinkOnLeftPane = this.FindLinkById (ElementIds.ID_HOME_PAGE_CODE_ANCHOR_ON_LEFT_PANE);
			this.CodeLinkOnLeftPane.Click ();

			this.searchResults.Clear ();

			while (true)
			{
				Thread.Sleep (5000);

				// Find all the search result blocks.
				var searchResultsBlocks = base.FindAllDivsByCss (CssClasses.CSS_SEARCH_RESULT_BLOCK);

				// If "show more" links are present, click them so that all matching lines are shown in the screen.
				var showMoreLinks = base.GetAllElementsByCss (CssClasses.CSS_SHOW_MORE_ENTRIES_LINK.RefineForAnchor ());

				if (showMoreLinks != null && showMoreLinks.Count > 0)
				{
					foreach (var oneLink in showMoreLinks)
					{
						var showMoreLink = new Link (oneLink, string.Empty, base.webDriver);
						if (showMoreLink.WebElement.IsAnchorClickable ())
						{
							showMoreLink.Click ();
						}
					}
				}

				var blockCounter = 0;

				if (searchResultsBlocks.Count > 0)
				{
					foreach (var oneBlock in searchResultsBlocks)
					{
						if (blockCounter == 0)
						{
							// The first search result happens to be the "container" DIV that houses all the individual search blocks, which
							// also happens to have the same CSS class as the search blocks. So, we can safely ignore this DIV.
							blockCounter++;
							continue;
						}

						var blockHtml = oneBlock.WebElement.GetAttribute ("outerHTML");

						if (
							blockHtml.AllOfThemPresent (CssClasses.CSS_PROJECT_NAME_LINK, CssClasses.CSS_FILE_NAME_LINK) == false
							|| blockHtml.AnyOfThemPresent (CssClasses.CSS_LINES_BLOCK_DIV, CssClasses.CSS_LINES_BLOCK_DIV2) == false
							)
						{
							// The search result block is not in the expected format. So, we can safely ignore this block.
							continue;
						}

						var fileLink = oneBlock.WebElement.FindElement (By.CssSelector (CssClasses.CSS_FILE_NAME_LINK.RefineForAnchor ()));
						var fileName = fileLink.GetInnerText (base.webDriver);

						var extension = Path.GetExtension (fileName);

						if (this.confineExtensions != null && this.confineExtensions.Count > 0)
						{
							if (this.confineExtensions.Contains (extension) == false)
							{
								// The file extension is not in the list of extensions to be confined to. So, we can safely ignore this block.
								continue;
							}
						}

						var projectNameLink = oneBlock.WebElement.FindElement (By.CssSelector (CssClasses.CSS_PROJECT_NAME_LINK.RefineForAnchor ()));

						IWebElement fileLinesBlock = default;

						try
						{
							fileLinesBlock = oneBlock.WebElement.FindElement (By.CssSelector (CssClasses.CSS_LINES_BLOCK_DIV.RefineForDiv ()));
						}
						catch
						{
							fileLinesBlock = oneBlock.WebElement.FindElement (By.CssSelector (CssClasses.CSS_LINES_BLOCK_DIV2.RefineForDiv ()));
						}

						var fileLinesBlockText = fileLinesBlock.GetAttribute ("outerHTML");

						if (fileLinesBlockText.AnyOfThemPresent (CssClasses.CSS_LINE_NUMBER_SPAN, CssClasses.CSS_LINE_NUMBER_SPAN2) == false)
						{
							// The file lines block is not in the expected format. So, we can safely ignore this block.
							continue;
						}

						var projectName = projectNameLink.GetInnerText (base.webDriver);

						var matchingStatements = new List<MatchingStatement> ();

						// Get the line numbers.
						List<IWebElement> lineNumberSpans = default;
						IList<IWebElement> lineNumberSpansSetOne = default;
						IList<IWebElement> lineNumberSpansSetTwo = default;

						try
						{
							lineNumberSpansSetOne = fileLinesBlock.FindElements (By.CssSelector (CssClasses.CSS_LINE_NUMBER_SPAN.RefineForSpan ()));
						}
						catch
						{
						}

						try
						{
							lineNumberSpansSetTwo = fileLinesBlock.FindElements (By.CssSelector (CssClasses.CSS_LINE_NUMBER_SPAN2.RefineForSpan ()));
						}
						catch
						{
						}

						lineNumberSpans = new List<IWebElement> ();
						lineNumberSpans.AddRange (lineNumberSpansSetOne);
						lineNumberSpans.AddRange (lineNumberSpansSetTwo);

						// Get the matching statements.
						var statementSpans = fileLinesBlock.FindElements (By.CssSelector (CssClasses.CSS_LINE_TEXT_SPAN.RefineForSpan ()));

						if (lineNumberSpans.Count == statementSpans.Count)
						{
							for (int i = 0; i < lineNumberSpans.Count; i++)
							{
								var countSpan = lineNumberSpans [i];
								var statementSpan = statementSpans [i];

								var countAsText = countSpan.Text.Trim ();
								var statement = statementSpan.Text.Trim ();

								if (string.IsNullOrWhiteSpace (countAsText) == false && string.IsNullOrWhiteSpace (statement) == false)
								{
									matchingStatements.Add (new MatchingStatement (countAsText, statement));
								}
							}
						}

						var searchResultEntry = new GitHubSearchResultEntry (projectName, fileName, matchingStatements, extension);
						this.searchResults.Add (searchResultEntry);
					}
				}
				else
				{
					// Count of search blocks is zero. Break the WHILE loop.
					break;
				}

				IList<IWebElement> paginationButtons = default;

				try
				{
					paginationButtons = base.GetAllElementsByCss (CssClasses.CSS_NEXT_PAGE_LINK.RefineForAnchor ());
				}
				catch
				{
				}

				if (paginationButtons == null || paginationButtons.Count == 0)
				{
					// No more pages to navigate to. Break the WHILE loop.
					break;
				}

				var nextPageButton = new Link (paginationButtons [paginationButtons.Count - 1], string.Empty, base.webDriver);

				if (nextPageButton.WebElement.IsAnchorClickable ())
				{
					nextPageButton.Click ();
				}
				else
				{
					// The next page button is not clickable. Break the WHILE loop.
					break;
				}
			}
		}
	}
}