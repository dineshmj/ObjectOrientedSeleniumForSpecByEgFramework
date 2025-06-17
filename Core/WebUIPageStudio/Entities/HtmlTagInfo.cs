using OOSelenium.Framework.Extensions;
using System.ComponentModel;

using OOSF = OOSelenium.Framework.WebUIControls;

namespace OOSelenium.WebUIPageStudio.Entities
{
	public sealed class HtmlTagInfo
		: INotifyPropertyChanged
	{
		public string? Tag { get; set; }

		public string? Text { get; set; }

		public string? Id { get; set; }

		public string? CssClassName { get; set; }

		public string? Name { get; set; }

		public string? Value { get; set; }

		public string? Source { get; set; }

		public string? LinkURL { get; set; }

		public string? Type { get; set; }

		public string? XPath { get; set; }

		public string? ParentTag { get; set; }

		public string? ParentName { get; set; }

		public string? ParentXPath { get; set; }

		public bool ParentHasMultiple { get; set; }

		public TagRenderArea TagRenderArea { get; set; }

		public Bitmap TagRenderImage { get; set; }

		public string? Description { get { return this.ToString (); }}

		public string UserSuggestedPropertyName { get; set; }

		public event PropertyChangedEventHandler? PropertyChanged;

		protected void OnPropertyChanged (string propertyName)
		{
			PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
		}

		public override string ToString ()
		{
			switch (this.Tag?.ToLowerInvariant ())
			{
				case "button":
					return $"{typeof (OOSF.Button).Name} '{this.Text}'";

				case "input":
					if (this.Type?.ToLowerInvariant () == "submit")
					{
						return $"{typeof (OOSF.SubmitButton).Name} '{this.Value}'";
					}
					else if (this.Type?.ToLowerInvariant () == "checkbox")
					{
						return $"{typeof (OOSF.CheckBox).Name} '{this.Value}'";
					}
					else if (this.Type?.ToLowerInvariant () == "radio")
					{
						return $"{typeof (OOSF.RadioButtons).Name} '{this.Name}'";
					}
					else
					{
						return $"{typeof (OOSF.TextField).Name} '{this.Id}'";
					}

				case "div":
					return $"{typeof (OOSF.Div).Name} '{this.CssClassName}'";

				case "select":
					return $"{typeof (OOSF.DropDownList).Name} '{this.Name}'";

				case "h1":
					return $"{typeof (OOSF.HeaderOne).Name} '{this.Text}'";

				case "h2":
					return $"{typeof (OOSF.HeaderTwo).Name} '{this.Text}'";

				case "h3":
					return $"{typeof (OOSF.HeaderThree).Name} '{this.Text}'";

				case "h4":
					return $"{typeof (OOSF.HeaderFour).Name} '{this.Text}'";

				case "h5":
					return $"{typeof (OOSF.HeaderFive).Name} '{this.Text}'";

				case "h6":
					return $"{typeof (OOSF.HeaderSix).Name} '{this.Text}'";

				case "label":
					return $"{typeof (OOSF.Label).Name} '{this.Text}'";

				case "legend":
					return $"{typeof (OOSF.Legend).Name} '{this.Text}'";

				case "a":
					return $"{typeof (OOSF.Link).Name} '{this.LinkURL}'";

				case "link":
					return $"{typeof (OOSF.Link).Name} '{this.Text}'";

				case "option":
					if (this.ParentTag?.ToLowerInvariant () == "select" && this.ParentHasMultiple)
					{
						return $"{typeof (OOSF.MultiSelectListBox).Name} '{this.ParentName}'";
					}
					break;

				case "p":
					return $"{typeof (OOSF.Paragraph).Name} '{this.Text}'";

				case "textarea":
					return $"{typeof (OOSF.TextArea).Name} '{this.Text}'";

				case "img":
					return $"{typeof (OOSF.Image).Name} '{this.Source.Substring (this.Source.LastIndexOf ('/') + 1)}'";

				case "span":
					return $"{typeof (OOSF.Span).Name} '{this.Text}'";

				case "table":
					return $"{typeof (OOSF.Table).Name} '{this.Name}'";
			}

			return $"Un-supported Tag '{this.Tag}'";
		}
	}
}