namespace OOSelenium.Framework.Entities
{
	public sealed class TextValuePair
	{
		public string Text { get; private set; }
		public string Value { get; private set; }

		public TextValuePair (string text, string value)
		{
			this.Text = text;
			this.Value = value;
		}
	}
}
