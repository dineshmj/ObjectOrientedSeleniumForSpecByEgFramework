using OOSelenium.Framework.Extensions;
using System.ComponentModel;

using OOSF = OOSelenium.Framework.WebUIControls;

namespace OOSelenium.WebUIPageStudio.Entities
{
	public sealed class HtmlTagInfo
		: INotifyPropertyChanged
	{
		// Private fields.
		private string? _tag;
		private XPathInfo? _xPathPathInfo;
		private bool _isMultiSelectListBox;

		// Public properties.
		public string? Tag
		{
			get { return _tag; }
			set
			{
				this.CorrectTagAndXPathForMultiSelectListBoxes (this.XPathInfo);
				_tag = value;
			}
		}

		public string? Text { get; set; }

		public string? Id { get; set; }

		public string? CssClassName { get; set; }

		public string? Name { get; set; }

		public string? Value { get; set; }

		public string? Source { get; set; }

		public string? LinkURL { get; set; }

		public string? Type { get; set; }

		public XPathInfo? XPathInfo
		{
			get { return _xPathPathInfo; }
			set
			{
				this.CorrectTagAndXPathForMultiSelectListBoxes (value);
				_xPathPathInfo = value;
			}
		}

		public string? ParentTag { get; set; }

		public string? ParentName { get; set; }

		public XPathInfo? ParentXPathInfo { get; set; }

		public bool ParentHasMultiple { get; set; }

		public TagRenderArea? TagRenderArea { get; set; }

		public Bitmap? TagRenderImage { get; set; }

		public string? Description { get { return this.ToString (); }}

		public string? UserSuggestedPropertyName { get; set; }

		public string? NearbyRadioId { get; set; }

		public string? NearbyRadioName { get; set; }

		public XPathInfo? NearbyRadioXPathInfo { get; set; }

		public string? NearbyCheckBoxId { get; set; }

		public string? NearbyCheckBoxName { get; set; }

		public XPathInfo? NearbyCheckBoxXPathInfo { get; set; }

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
					if (_isMultiSelectListBox)
					{
						return $"{typeof (OOSF.MultiSelectListBox).Name} '{this.Name}'";
					}

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

		private void CorrectTagAndXPathForMultiSelectListBoxes (XPathInfo? xPathInfo)
		{
			if (this.Tag?.ToLowerInvariant () == "option" && xPathInfo != null)
			{
				// Check the "XPathByDomPath" property of value. If it ends with "/option", "/option[2]", etc. (case-insensitive) and its
				// predecessor (or one or two levels above) is a "select", then the "XPathByDomPath" has to be modified
				// such that it is only till "/select", and the name of the tag is to be changed from "option" to "select".

				var xPathParts = xPathInfo.XPathByDomPath.Split (new [] { '/' }, StringSplitOptions.None);

				var lastIndex = xPathParts.Length - 1;
				var parentIndex = lastIndex - 1; // The parent element is the one before the last one.
				var grandParentIndex = lastIndex - 2; // The grandparent element is the one before the parent.
				var greatGrandParentIndex = lastIndex - 3; // The great-grandparent element is the one before the grandparent.

				if (greatGrandParentIndex < 0)
				{
					return; // Not enough elements to check for a "select" tag.
				}

				var lastPart = xPathParts [lastIndex].ToLowerInvariant ();
				var parentPart = xPathParts [parentIndex].ToLowerInvariant ();
				var grandParentPart = xPathParts [grandParentIndex].ToLowerInvariant ();
				var greatGrandParentPart = xPathParts [greatGrandParentIndex].ToLowerInvariant ();

				if (lastPart == "option" || lastPart.StartsWith ("option[", StringComparison.OrdinalIgnoreCase))
				{
					var newXPath = string.Empty;

					if (parentPart == "select" || parentPart.StartsWith ("select[", StringComparison.OrdinalIgnoreCase))
					{
						// Join the xPathParts till parent part's index and form the new xPath.
						newXPath = string.Join ("/", xPathParts, 0, parentIndex + 1);
					}
					else if (grandParentPart == "select" || grandParentPart.StartsWith ("select[", StringComparison.OrdinalIgnoreCase))
					{
						// Join the xPathParts till grandparent part's index and form the new xPath.
						newXPath = string.Join ("/", xPathParts, 0, grandParentIndex + 1);
					}
					else if (greatGrandParentPart == "select" || greatGrandParentPart.StartsWith ("select[", StringComparison.OrdinalIgnoreCase))
					{
						// Join the xPathParts till great-grandparent part's index and form the new xPath.
						newXPath = string.Join ("/", xPathParts, 0, greatGrandParentIndex + 1);
					}
					else
					{
						return; // Not a multi-select list box.
					}

					if (newXPath.IsNotNullEmptyOrWhitespace ())
					{
						// Update the XPathByDomPath property with the new XPath.
						xPathInfo.XPathByDomPath = newXPath;
						this.Tag = "select";
						_isMultiSelectListBox = true;
					}
				}
			}
		}
	}
}