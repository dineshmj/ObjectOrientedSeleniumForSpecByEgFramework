namespace SimpleQA.Tools.WebUIPageStudio.Entities
{
	public sealed class XPathInfo
	{
		public string? XPathById { get; set; }

		// Relevant for Pega web applications
		public string? XPathByDataTestId { get; set; }

		public string XPathByName { get; set; }

		public string? XPathByCssClass { get; set; }

		public string? XPathByDomPath { get; set; }
	}
}