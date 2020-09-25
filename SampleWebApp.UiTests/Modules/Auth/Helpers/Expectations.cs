namespace SampleWebApp.UiTests.Modules.Auth.Helpers
{
	public static class Expectations
	{
		// Sign in screen.
		public const string SIGN_IN_PAGE_TITLE = "Sign in to Insurance One by Four Walls Inc.";
		public const string SIGN_IN_PAGE_USER_ID_FIELD_LABEL_TEXT = "User ID";
		public const string SIGN_IN_PAGE_PASSWORD_FIELD_LABEL_TEXT = "Password";
		public const string SIGN_IN_PAGE_SIGN_IN_BUTTON_TEXT = "Sign in";

		// Failed signing in attempt.
		public const string INVALID_USER_ID_OR_PASSWORD_MESSAGE = "Invalid user ID or password.";

		// Quote issuer landing page screen.
		public const string QUOTE_ISSUER_PAGE_TITLE = "Welcome to Insurance One!";
		public const string QUOTE_ISSUER_PAGE_HOME_LINK_TEXT = "Insurance One";
		public const string QUOTE_ISSUER_PAGE_SIGN_OUT_LINK_TEXT = "Sign Out";
		public const string QUOTE_ISSUER_PAGE_SEARCH_QUOTES_LINK_TEXT = "Search quotes";
		public const string QUOTE_ISSUER_PAGE_ISSUE_A_NEW_QUOTE_LINK_TEXT = "Issue a new quote";
	}
}