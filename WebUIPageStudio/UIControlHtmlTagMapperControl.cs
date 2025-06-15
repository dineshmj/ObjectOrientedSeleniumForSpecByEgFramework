using OOSelenium.Framework.WebUIControls;
using OOSelenium.WebUIPageStudio.Entities;
using OOSelenium.WebUIPageStudio.Helpers;
using OOSelenium.WebUIPageStudio.Resources;

namespace OOSelenium.WebUIPageStudio
{
	public partial class UIControlHtmlTagMapperControl
		: UserControl
	{
		private HtmlTagInfo htmlTagInfo;
		private readonly IDictionary<string, Type> webUiControlsMappingDictionary = new Dictionary<string, Type> ();

		public UIControlHtmlTagMapperControl ()
		{
			this.InitializeComponent ();
			WebUIControlsHelper
				.GetSupportedWebUiControls ()
				.ToList ()
				.ForEach (supportedWebUiControlType =>
				{
					// Add the control type to the dictionary.
					if (!this.webUiControlsMappingDictionary.ContainsKey (supportedWebUiControlType.Name))
					{
						this.webUiControlsMappingDictionary.Add (supportedWebUiControlType.Name, supportedWebUiControlType);
					}
				});
		}

		public void MapHtmlTagInfo (HtmlTagInfo htmlTagInfo, int index, int totalCount)
		{
			if (htmlTagInfo == null)
			{
				throw new ArgumentNullException (nameof (htmlTagInfo));
			}

			this.htmlTagInfo = htmlTagInfo;

			if (index < 1 || index > totalCount)
			{
				throw new ArgumentOutOfRangeException (nameof (index), "Index must be between 1 and total number of HTML Tag Info instances in the collection.");
			}

			// Map the HTML tag info to the UI controls.
			this.nOfTotalLabel.Text = $"({index} of {totalCount})";
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

				if (this.webUiControlsMappingDictionary.TryGetValue (controlTypeAsText, out var controlType))
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
			var firstChar = pageModelPropertyNameTextBox.Text? [0];
			var isNameOk
				= firstChar >= 'A'
					&& firstChar <= 'Z'
					&& pageModelPropertyNameTextBox.Text.EndsWith (this.mappedControlNameValueLabel.Text);

			this.nameOkPictureBox.Image
				= isNameOk
					? StudioResources.Ok
					: StudioResources.NotOk;

			if (!isNameOk)
			{
				this.whyNameNotOkTooltip.ToolTipTitle = "Invalid name for Page Model Proprty";
				this.whyNameNotOkTooltip.SetToolTip (this.nameOkPictureBox, $"Page Model Proprty's name must begin with an Upper case alphabetic character, and end with '{this.mappedControlNameValueLabel.Text}'.");
			}
			else
			{
				this.htmlTagInfo.UserSuggestedPropertyName = this.pageModelPropertyNameTextBox.Text;
				this.whyNameNotOkTooltip.RemoveAll ();
			}
		}
	}
}