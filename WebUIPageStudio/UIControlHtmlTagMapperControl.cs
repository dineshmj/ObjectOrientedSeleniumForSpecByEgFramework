using OOSelenium.Framework.Abstractions;
using OOSelenium.WebUIPageStudio.Entities;
using OOSelenium.WebUIPageStudio.Helpers;
using OOSelenium.WebUIPageStudio.Resources;

using OOSF = OOSelenium.Framework.WebUIControls;

namespace OOSelenium.WebUIPageStudio
{
	public partial class UIControlHtmlTagMapperControl
		: UserControl
	{
		private HtmlTagInfo htmlTagInfo;
		private readonly IList<Type> webUiControlTypes
			= [
				typeof (OOSF.Button),
				typeof (OOSF.CheckBox),
				typeof (OOSF.Div),
				typeof (OOSF.DropDownList),
				typeof (OOSF.HeaderOne),
				typeof (OOSF.HeaderTwo),
				typeof (OOSF.HeaderThree),
				typeof (OOSF.HeaderFour),
				typeof (OOSF.HeaderFive),
				typeof (OOSF.HeaderSix),
				typeof (OOSF.Image),
				typeof (OOSF.Label),
				typeof (OOSF.Legend),
				typeof (OOSF.Link),
				typeof (OOSF.MultiSelectListBox),
				typeof (OOSF.RadioButtons),
				typeof (OOSF.Span),
				typeof (OOSF.SubmitButton),
				typeof (OOSF.TextArea),
				typeof (OOSF.TextField),
				typeof (OOSF.ValidationLabel),
				typeof (OOSF.ValidationSummary)
			];
		private readonly IDictionary<string, Type> webUiControlsDictionary = new Dictionary<string, Type> ();

		public UIControlHtmlTagMapperControl ()
		{
			InitializeComponent ();
			this.webUiControlTypes.ToList ().ForEach (type =>
			{
				// Add the control type to the dictionary.
				if (!this.webUiControlsDictionary.ContainsKey (type.Name))
				{
					this.webUiControlsDictionary.Add (type.Name, type);
				}
			});
		}

		public void MapHtmlTagInfo (HtmlTagInfo htmlTagInfo, int index, int total)
		{
			if (htmlTagInfo == null)
			{
				throw new ArgumentNullException (nameof (htmlTagInfo));
			}

			this.htmlTagInfo = htmlTagInfo;

			if (index < 0 || index > total)
			{
				throw new ArgumentOutOfRangeException (nameof (index), "Index must be between 0 and total number of HTML Tag Info instances in the collection.");
			}

			// Map the HTML tag info to the UI controls.
			this.nOfTotalLabel.Text = $"({index + 1} of {total})";
			this.htmlTagNameValueLabel.Text = htmlTagInfo.Description;
			this.mappedControlNameValueLabel.Text = this.MapOosfControlNameFrom (htmlTagInfo.Description);
			this.pageModelPropertyNameTextBox.Text = $"{this.ExtractDescriptionFrom (htmlTagInfo.Description).FormPascalCaseNameFromDescription ()}{this.mappedControlNameValueLabel.Text}";
			this.previewPictureBox.Image = htmlTagInfo.TagRenderImage;
		}

		private string MapOosfControlNameFrom (string? description)
		{
			var firstSingleQuoteIndex = description?.IndexOf ('\'');

			if (firstSingleQuoteIndex.HasValue && firstSingleQuoteIndex.Value >= 0)
			{
				// Extract the control name from the description.
				var controlTypeAsText = description.Substring (0, firstSingleQuoteIndex.Value).Trim ();

				if (this.webUiControlsDictionary.TryGetValue (controlTypeAsText, out var controlType))
				{
					return controlType.Name;
				}
			}

			throw new ArgumentException ("Could not map an OOSF type for specified description.", nameof (description));
		}

		private string ExtractDescriptionFrom (string? description)
		{
			var firstSingleQuoteIndex = description?.IndexOf ('\'');
			var lastSingleQuoteIndex = description?.LastIndexOf ('\'');

			if (firstSingleQuoteIndex.HasValue && lastSingleQuoteIndex.HasValue &&
				firstSingleQuoteIndex.Value >= 0 && lastSingleQuoteIndex.Value > firstSingleQuoteIndex.Value)
			{
				// Extract the control name from the description.
				return description.Substring (firstSingleQuoteIndex.Value + 1, lastSingleQuoteIndex.Value - firstSingleQuoteIndex.Value - 1);
			}

			throw new ArgumentException ("Description does not contain a valid tag description enclosed in single quotes.", nameof (description));
		}

		private void pageModelPropertyNameTextBox_TextChanged (object sender, EventArgs e)
		{
			var isNameOk = pageModelPropertyNameTextBox.Text.EndsWith (this.mappedControlNameValueLabel.Text);

			this.nameOkPictureBox.Image
				= isNameOk
					? StudioResources.Ok
					: StudioResources.NotOk;

			if (!isNameOk)
			{
				this.whyNameNotOkTooltip.SetToolTip (this.nameOkPictureBox, $"Page Model Proprty's name must end with '{this.mappedControlNameValueLabel.Text}'.");
			}
			else
			{
				this.htmlTagInfo.UserSuggestedPropertyName = this.pageModelPropertyNameTextBox.Text;
				this.whyNameNotOkTooltip.RemoveAll ();
			}
		}
	}
}