using System.ComponentModel;

namespace OOSelenium.WebUIPageStudio.Entities
{
	public sealed class HtmlTagInfo
		: INotifyPropertyChanged
	{
		public string? Tag { get; set; }

		public string? Text { get; set; }

		public string? Id { get; set; }

		public string? CssClass { get; set; }

		public string? Name { get; set; }

		public string? Source { get; set; }

		public string? LinkURL { get; set; }

		public string? Type { get; set; }

		public string? XPath { get; set; }

		public TagRenderArea TagRenderArea { get; set; }

		public Bitmap TagRenderImage { get; set; }

		public string? Description { get { return this.ToString (); }}

		public event PropertyChangedEventHandler? PropertyChanged;

		protected void OnPropertyChanged (string propertyName)
		{
			PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
		}

		public override string ToString ()
		{
			switch (this.Tag?.ToLowerInvariant ())
			{
				case "a":
					return $"Link '{this.Text}'";

				case "button":
					return $"Button '{this.Text}'";

				case "input":
					if (this.Type?.ToLowerInvariant () == "submit")
					{
						return $"Submit Button '{this.Text}'";
					}
					else if (this.Type?.ToLowerInvariant () == "checkbox")
					{
						return $"Checkbox '{this.Text}'";
					}
					else if (this.Type?.ToLowerInvariant () == "radio")
					{
						return $"Radio Button '{this.Text}'";
					}
					else
					{
						return $"Textbox '{this.Text}'";
					}

				case "select":
					return $"Dropdown '{this.Text}'";

				case "textarea":
					return $"Textarea '{this.Text}'";

				case "img":
					return $"Image '{this.Source.Substring (this.Source.LastIndexOf ('/') + 1)}'";

				case "label":
					return $"Label '{this.Text}'";

				case "form":
					return $"Form '{this.Text}'";

				case "table":
					return $"Table '{this.Text}'";

				case "tr":
					return $"Table Row '{this.Text}'";

				case "td":
					return $"Table Cell '{this.Text}'";

				case "ul":
					return $"Unordered List '{this.Text}'";

				case "ol":
					return $"Ordered List '{this.Text}'";

				case "li":
					return $"List Item '{this.Text}'";

				case "div":
					return $"Div";

				case "span":
					return $"Span '{this.Text}'";

				case "header":
					return $"Header";

				case "footer":
					return $"Footer";

				case "nav":
					return $"Navigation '{this.Text}'";

				case "section":
					return $"Section '{this.Text}'";

				case "article":
					return $"Article '{this.Text}'";

				case "aside":
					return $"Aside '{this.Text}'";

				case "h1":
				case "h2":
				case "h3":
				case "h4":
				case "h5":
				case "h6":
					return $"Heading '{this.Text}'";

				case "p":
					return $"Paragraph '{this.Text}'";

				case "video":
					return $"Video '{this.Text}'";

				case "audio":
					return $"Audio '{this.Text}'";

				case "canvas":
					return $"Canvas '{this.Text}'";

				case "svg":
					return $"SVG '{this.Text}'";

				case "iframe":
					return $"Iframe '{this.Text}'";

				case "script":
					return $"Script '{this.Text}'";

				case "link":
					return $"Link '{this.Text}'";

				case "meta":
					return $"Meta Tag '{this.Text}'";

				case "style":
					return $"Style Tag '{this.Text}'";

				default:
					return $"Tag '{this.Text}'";
			}
		}
	}
}