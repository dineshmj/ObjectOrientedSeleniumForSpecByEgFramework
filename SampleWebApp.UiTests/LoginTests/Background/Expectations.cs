namespace SampleWebApp.UiTests.LoginTests.Background
{
	public static class Expectations
	{
		// Sign in screen.
		public const string LOGIN_PAGE_TITLE = "Sign in to Insurance One by Four Walls Inc.";

		public const string LOGIN_PAGE_USER_ID_FIELD_LABEL_TEXT = "User ID";
		public const string LOGIN_PAGE_PASSWORD_FIELD_LABEL_TEXT = "Password";

		public const string LOGIN_PAGE_USER_ID_FIELD_PLACEHOLDER = "Enter user ID";
		public const string LOGIN_PAGE_PASSWORD_FIELD_PLACEHOLDER = "Enter password";

		public const string LOGIN_PAGE_LOGIN_BUTTON_TEXT = "Sign in";

		// Failed signing in attempt.
		public const string INVALID_USER_ID_OR_PASSWORD_MESSAGE = "Invalid user ID or password.";

		// Quote issuer landing page screen.
		public const string QUOTE_ISSUER_PAGE_TITLE = "Welcome to Insurance One!";
		public const string QUOTE_ISSUER_PAGE_HOME_LINK_TEXT = "Insurance One";
		public const string QUOTE_ISSUER_PAGE_LOGOUT_LINK_TEXT = "Sign Out";
		public const string QUOTE_ISSUER_PAGE_SEARCH_QUOTES_LINK_TEXT = "Search quotes";
		public const string QUOTE_ISSUER_PAGE_ISSUE_A_NEW_QUOTE_LINK_TEXT = "Issue a new quote";
	}
}