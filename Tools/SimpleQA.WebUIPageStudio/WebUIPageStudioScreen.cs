using System.ComponentModel;
using System.Text.Json;

using Microsoft.Web.WebView2.Core;

using SimpleQA.Framework.Extensions;
using SimpleQA.Tools.WebUIPageStudio.Entities;
using SimpleQA.Tools.WebUIPageStudio.Helpers;
using SimpleQA.Tools.WebUIPageStudio.Resources;

namespace SimpleQA.Tools.WebUIPageStudio
{
	public partial class WebUIPageStudioScreen
		: Form
	{
		private HtmlTagInfo? receivedElementInfo;
		private BindingList<HtmlTagInfo> selectedElements = [];
		private float displayScalingFactor;
		private string suggestedPageModelName;

		public WebUIPageStudioScreen ()
		{
			this.InitializeComponent ();
		}

		private async void WebUIPageStudioScreen_Load (object sender, EventArgs e)
		{
			this.displayScalingFactor = DpiHelper.GetScalingFactor (this);

			// Listbox for selected elements binding
			this.selectedElementsListBox.DataSource = this.selectedElements;
			this.selectedElementsListBox.DisplayMember = nameof (HtmlTagInfo.Description);

			// Set the initial image for the tag render area
			await this.appPageWebView.EnsureCoreWebView2Async ();

			// Event handlers for the WebView2 control
			this.appPageWebView.CoreWebView2.NavigationStarting += CoreWebView2_NavigationStarting;
			this.appPageWebView.CoreWebView2.NavigationCompleted += this.CoreWebView2_NavigationCompleted;

			this.appPageWebView.CoreWebView2.ContextMenuRequested += this.appPageWebView_ContextMenuRequested;
			this.appPageWebView.CoreWebView2.WebMessageReceived += this.appPageWebView_WebMessageReceived;

			this.StartFresh ();
		}

		private void CoreWebView2_NavigationStarting (object? sender, CoreWebView2NavigationStartingEventArgs e)
		{
			// If the user has selected elements on the current page, prompt them before navigating away.
			if (this.selectedElements.Count > 0)
			{
				var result = MessageBox.Show (
					"You have selected elements on this page. Navigating to a new page will clear them.\n\nDo you want to continue?",
					"Confirm Navigation",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Warning);

				if (result == DialogResult.No)
				{
					e.Cancel = true;
					return;
				}
			}

			// Flash "page is loading" label.
			this.pageIsLoadingLabel.Visible = true;
			this.pageLoadingTimer.Enabled = false;
			this.pageLoadingTimer.Start ();

			// The user wants to navigate to a new page, so we clear the current selections.
			this.StartFresh ();
		}

		private async void CoreWebView2_NavigationCompleted (object? sender, CoreWebView2NavigationCompletedEventArgs e)
		{
			this.pageIsLoadingLabel.Visible = false;
			this.pageLoadingTimer.Stop ();
			this.pageLoadingTimer.Enabled = false;

			string js = @"
                (function() {
					function buildXPathInfo (element) {
						if (!element || !element.tagName) {
							return null;
						}

						xPathInfo = {
							xPathById: null,
							xPathByDataTestId: null,
							xPathByName: null,
							xPathByCssClass: null,
							xPathByDomPath: null
						};

						if (element === document.body) {
							xPathInfo.xPathByDomPath = '/html/body';
							return xPathInfo;
						}

						const id = element.getAttribute('id');
						if (id) {
							xPathInfo.xPathById = `//*[@id='${id}']`;
						}

						const dataTestId = element.getAttribute('data-testid');
						if (dataTestId) {
							xPathInfo.xPathByDataTestId = `//*[@data-testid='${dataTestId}']`;
						}

						const name = element.getAttribute('name');
						if (name) {
							xPathInfo.xPathByName = `//*[@name='${name}']`;
						}

						const className = element.className;
						if (className) {
							const classNames = className.split(' ').map(c => c.trim()).filter(c => c.length > 0);
							if (classNames.length > 0) {
								xPathInfo.xPathByCssClass = classNames.map(c => `//*[contains(concat(' ', normalize-space(@class), ' '), ' ${c} ')]`).join(' | ');
							}
						}

						xPathInfo.xPathByDomPath = getXPath(element);
						return xPathInfo;
					}

                    function getXPath(element) {
						if (element === document.documentElement) {
							return '/html';
						}

						if (element === document.head) {
							return '/html/head';
						}

                        if (element === document.body) {
                            return '/html/body';
                        }

                        let ix = 0;
                        let siblings = element.parentNode.childNodes;

                        for (let i = 0; i < siblings.length; i++) {
                            let sibling = siblings[i];
                            if (sibling === element) {
                                let path = getXPath(element.parentNode) + '/' + element.tagName.toLowerCase();
                                if (ix > 0) {
                                    path += '[' + (ix + 1) + ']';
                                }
                                return path;
                            }

                            if (sibling.nodeType === 1 && sibling.tagName === element.tagName) {
                                ix++;
                            }
                        }
                    }

					function getNearByRadioOrCheckbox (target, type) {
						if (target?.parentElement) {
							siblings = target.parentElement.children;

							for (let i = 0; i < siblings.length; i++) {
								const sibling = siblings[i];
								const typeValue = sibling?.getAttribute('type')?.toLowerCase ();

								if (sibling!= target && sibling?.tagName?.toLowerCase() === 'input' && typeValue === type) {
									return sibling;
								}
							}
						}

						if (target?.parentElement?.parentElement) {
							siblings = target.parentElement.parentElement.children;

							for (let j = 0; j < siblings.length; j++) {
								const sibling = siblings[j];
								const typeValue = sibling?.getAttribute('type')?.toLowerCase ();

								if (sibling!= target && sibling?.tagName?.toLowerCase() === 'input' && typeValue === type) {
									return sibling;
								}
							}
						}

						if (target?.parentElement?.parentElement?.parentElement) {
							siblings = target.parentElement.parentElement.parentElement.children;

							for (let k = 0; k < siblings.length; k++) {
								const sibling = siblings[k];
								const typeValue = sibling?.getAttribute('type')?.toLowerCase ();

								if (sibling!= target && sibling?.tagName?.toLowerCase() === 'input' && typeValue === type) {
									return sibling;
								}
							}
						}

						return null;
					}

                    document.addEventListener('contextmenu', function(e) {
                        const el = e.target;
						const parent = el.parentElement;

						// Get nearby radio or checkbox if present.
						const nearByRadio = getNearByRadioOrCheckbox(el, 'radio');
						const nearByCheckbox = getNearByRadioOrCheckbox(el, 'checkbox');

						const rect = el.getBoundingClientRect();

                        const details = {
                            " + nameof (HtmlTagInfo.Tag) + @": el.tagName,
							" + nameof (HtmlTagInfo.Text) + @": el.innerText,
                            " + nameof (HtmlTagInfo.Id) + @": el.id,
							" + nameof (HtmlTagInfo.Value) + @": el.value,
                            " + nameof (HtmlTagInfo.CssClassName) + @": el.className,
                            " + nameof (HtmlTagInfo.Name) + @": el.getAttribute('name'),
                            " + nameof (HtmlTagInfo.Source) + @": el.getAttribute('src'),
                            " + nameof (HtmlTagInfo.LinkURL) + @": el.getAttribute('href'),
                            " + nameof (HtmlTagInfo.Type) + @": el.getAttribute('type'),
                            " + nameof (HtmlTagInfo.XPathInfo) + @": el ? buildXPathInfo (el) : null,

							" + nameof (HtmlTagInfo.ParentTag) + @": parent ? parent.tagName : null,
							" + nameof (HtmlTagInfo.ParentId) + @": parent ? parent.getAttribute('id') : null,
							" + nameof (HtmlTagInfo.ParentName) + @": parent ? parent.getAttribute('name') : null,
							" + nameof (HtmlTagInfo.ParentValue) + @": parent ? parent.getAttribute('value') : null,
							" + nameof (HtmlTagInfo.ParentCssClassName) + @": parent ? parent.getAttribute('class') : null,
					        " + nameof (HtmlTagInfo.ParentHasMultiple) + @": parent ? parent.hasAttribute('multiple') : false,
							" + nameof (HtmlTagInfo.ParentXPathInfo) + @": parent? buildXPathInfo (parent) : null,

							" + nameof (HtmlTagInfo.NearbyRadioId) + @": nearByRadio ? nearByRadio?.getAttribute('id') : null,
							" + nameof (HtmlTagInfo.NearbyRadioName) + @": nearByRadio ? nearByRadio?.getAttribute('name') : null,
							" + nameof (HtmlTagInfo.NearbyRadioXPathInfo) + @": nearByRadio ? buildXPathInfo (nearByRadio) : null,
							" + nameof (HtmlTagInfo.NearbyCheckBoxId) + @": nearByCheckbox ? nearByCheckbox?.getAttribute('id') : null,
							" + nameof (HtmlTagInfo.NearbyCheckBoxName) + @": nearByCheckbox ? nearByCheckbox?.getAttribute('name') : null,
							" + nameof (HtmlTagInfo.NearbyCheckBoxXPathInfo) + @": nearByCheckbox ? buildXPathInfo (nearByCheckbox) : null,

							" + nameof (HtmlTagInfo.TagRenderArea) + @": {
								" + nameof (HtmlTagInfo.TagRenderArea.Top) + @": rect.top,
								" + nameof (HtmlTagInfo.TagRenderArea.Left) + @": rect.left,
								" + nameof (HtmlTagInfo.TagRenderArea.Width) + @": rect.width,
								" + nameof (HtmlTagInfo.TagRenderArea.Height) + @": rect.height,
								" + nameof (HtmlTagInfo.TagRenderArea.ClickX) + @": e.clientX,
								" + nameof (HtmlTagInfo.TagRenderArea.ClickY) + @": e.clientY
							}
                        };
                        window.chrome.webview.postMessage(details);
                    });
                })();
            ";

			await this.appPageWebView.ExecuteScriptAsync (js);

			this.suggestedPageModelName = $"{this.appPageWebView.CoreWebView2.DocumentTitle.GetAndJoinFirstTwoWords ()}Page";

			// Now that the user has navigated to the new URL, we can update the URL text box.
			this.appPageUrlTextBox.Text = this.appPageWebView?.Source?.ToString () ?? this.appPageUrlTextBox.Text;
		}

		private void appPageWebView_ContextMenuRequested (object? sender, CoreWebView2ContextMenuRequestedEventArgs e)
		{
			// Supress showing the default context menu
			e.Handled = true;

			if (this.receivedElementInfo != null)
			{
				const string UN_SUPPORTED_TAG = "Un-supported Tag";
				var customContextMenu = new ContextMenuStrip ();

				var elementDescription = string.IsNullOrWhiteSpace (this.receivedElementInfo.Description)
					? string.Empty
					: this.receivedElementInfo.Description;

				if (elementDescription.StartsWith (UN_SUPPORTED_TAG, StringComparison.OrdinalIgnoreCase))
				{
					var elementTagName
						= elementDescription
							.Replace (UN_SUPPORTED_TAG, string.Empty)
							.Replace ("'", string.Empty)
							.ToLowerInvariant ()
							.Trim ();

					var menuItemText = $"⚠ '{elementTagName}' is currently not supported. Consider selecting its parent ";

					switch (elementTagName)
					{
						case "th":
						case "tr":
						case "td":
						case "tbody":
							menuItemText = $"{menuItemText}table.";
							break;

						default:
							menuItemText = $"{menuItemText}element.";
							break;
					}

					customContextMenu.Items.Add (menuItemText, null, null);
				}
				else
				{
					// Check if the element is already in the selected elements list
					var elementAlreadyAdded = this.selectedElements.Any (x => x.XPathInfo.XPathByDomPath == this.receivedElementInfo.XPathInfo.XPathByDomPath);

					// Add an item to the context menu to add/remove the element
					customContextMenu.Items.Add ($"{(elementAlreadyAdded ? "Remove" : "Add")} {this.receivedElementInfo} element {(elementAlreadyAdded ? "from" : "to")} list", null, (s, args) =>
					{
						if (elementAlreadyAdded)
						{
							var elementToRemove = this.selectedElements.FirstOrDefault (x => x.XPathInfo.XPathByDomPath == this.receivedElementInfo.XPathInfo.XPathByDomPath);
							this.selectedElements.Remove (elementToRemove);
						}
						else
						{
							this.selectedElements.Add (this.receivedElementInfo);
						}

						// Update the tags count label
						this.toolStripTagsCountLabel.Text = $"{this.selectedElements.Count} elements selected";

						// Refresh the list box and select the newly added element
						this.selectedElementsListBox.SelectedIndex = this.selectedElementsListBox.Items.Count - 1;
						this.ShowElementPreviw ();

						// Enable the build page code button if there are selected elements
						this.buildPageCodeButton.Enabled = (this.selectedElements.Count > 0);

						this.receivedElementInfo = null;
					});
				}

				// Show the context menu at the cursor position
				customContextMenu.Show (Cursor.Position);
			}
		}

		private async void appPageWebView_WebMessageReceived (object sender, CoreWebView2WebMessageReceivedEventArgs e)
		{
			try
			{
				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				};

				// Deserialize the received JSON message into HtmlTagInfo object
				this.receivedElementInfo = JsonSerializer.Deserialize<HtmlTagInfo> (e.WebMessageAsJson, options);

				if (this.receivedElementInfo != null)
				{
					var nearByRadioPresent
						= this.receivedElementInfo.NearbyRadioId.IsNotNullEmptyOrWhitespace ()
							|| this.receivedElementInfo.NearbyRadioName.IsNotNullEmptyOrWhitespace ()
							|| (this.receivedElementInfo.NearbyRadioXPathInfo != null && this.receivedElementInfo.NearbyRadioXPathInfo.XPathByDomPath.IsNotNullEmptyOrWhitespace ());

					var nearByCheckBoxPresent
						= this.receivedElementInfo.NearbyCheckBoxId.IsNotNullEmptyOrWhitespace ()
							|| this.receivedElementInfo.NearbyCheckBoxName.IsNotNullEmptyOrWhitespace ()
							|| (this.receivedElementInfo.NearbyCheckBoxXPathInfo != null && this.receivedElementInfo.NearbyCheckBoxXPathInfo.XPathByDomPath.IsNotNullEmptyOrWhitespace ());

					if (nearByRadioPresent)
					{
						if (MessageBox.Show ("Did you mean to select the \"Radio\" button at this location?", "Radio Button Detected", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						{
							this.receivedElementInfo = new HtmlTagInfo
							{
								Tag = "input",
								Type = "radio",
								Id = this.receivedElementInfo.NearbyRadioId,
								Name = this.receivedElementInfo.NearbyRadioName,
								Text = this.receivedElementInfo.Text,
								Value = this.receivedElementInfo.Value,
								XPathInfo = this.receivedElementInfo.NearbyRadioXPathInfo,
								TagRenderArea = this.receivedElementInfo.TagRenderArea
							};
						}
					}
					else if (nearByCheckBoxPresent)
					{
						if (MessageBox.Show ("Did you mean to select the \"Check Box\" at this location?", "Check Box Detected", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						{
							this.receivedElementInfo = new HtmlTagInfo
							{
								Tag = "input",
								Type = "checkbox",
								Id = this.receivedElementInfo.NearbyCheckBoxId,
								Name = this.receivedElementInfo.NearbyCheckBoxName,
								Text = this.receivedElementInfo.Text,
								Value = this.receivedElementInfo.Value,
								XPathInfo = this.receivedElementInfo.NearbyCheckBoxXPathInfo,
								TagRenderArea = this.receivedElementInfo.TagRenderArea
							};
						}
					}

					// Capture the preview of rendered web page
					using var renderedPageStream = new MemoryStream ();
					await this.appPageWebView.CoreWebView2.CapturePreviewAsync (CoreWebView2CapturePreviewImageFormat.Png, renderedPageStream);
					renderedPageStream.Position = 0;

					using var renderedPageBitmap = new Bitmap (renderedPageStream);

					// If the receivedElementInfo is still null due to race condition, deserialize it again
					if (this.receivedElementInfo == null)
					{
						this.receivedElementInfo = JsonSerializer.Deserialize<HtmlTagInfo> (e.WebMessageAsJson, options);
					}

					// Original dimensions, scaled for display DPI
					int left = (int) (receivedElementInfo.TagRenderArea.Left * this.displayScalingFactor);
					int top = (int) (receivedElementInfo.TagRenderArea.Top * this.displayScalingFactor);
					int width = (int) (receivedElementInfo.TagRenderArea.Width * this.displayScalingFactor);
					int height = (int) (receivedElementInfo.TagRenderArea.Height * this.displayScalingFactor);

					// Padding: 80% of height
					int padding = (int) (height * 0.8f);

					// Apply that same padding to all four sides
					int newLeft = Math.Max (0, left - padding);
					int newTop = Math.Max (0, top - padding);
					int newWidth = Math.Min (renderedPageBitmap.Width - newLeft, width + (2 * padding));
					int newHeight = Math.Min (renderedPageBitmap.Height - newTop, height + (2 * padding));

					// Final crop rectangle
					var cropArea = new Rectangle (newLeft, newTop, newWidth, newHeight);

					// Ensure crop area is within bounds
					cropArea.Intersect (new Rectangle (Point.Empty, renderedPageBitmap.Size));

					// Obtain the cropped image corresponding to the element in question
					this.receivedElementInfo.TagRenderImage = renderedPageBitmap.Clone (cropArea, renderedPageBitmap.PixelFormat);
				}
			}
			catch
			{
				MessageBox.Show ($"Could not identify the HTML element on the page. Please try again.", "Element not identified", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void selectedElementsListBox_SelectedIndexChanged (object sender, EventArgs e)
		{
			this.ShowElementPreviw ();
		}

		private void navigateButton_Click (object sender, EventArgs e)
		{
			// Keep this text in Clipboard.
			Clipboard.SetText ("whatever you need in clipboard");

			// Is the URL in the text box the same as the current page URL?
			if (this.appPageUrlTextBox.Text == this.appPageWebView?.Source?.ToString ())
			{
				MessageBox.Show ("You're already on this page!", "Navigation not required", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			// Has the user selected any elements on the current page?
			if (this.selectedElements.Count > 0)
			{
				var result = MessageBox.Show (
					"You have selected elements on this page. Navigating to a new page will clear them.\n\nDo you want to continue?",
					"Confirm Navigation",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Warning);

				if (result == DialogResult.No)
				{
					return;
				}
			}

			try
			{
				// Now that the user has consented to navigate to the specified URL, we can clear the current selections.
				this.StartFresh ();

				if (this.appPageWebView.CoreWebView2 != null)
				{
					this.appPageWebView.CoreWebView2.Navigate (this.appPageUrlTextBox.Text);
				}
				else
				{
					this.appPageWebView.EnsureCoreWebView2Async ()
						.ContinueWith (t =>
						{
							if (t.Status == TaskStatus.RanToCompletion)
							{
								this.appPageWebView.CoreWebView2.Navigate (this.appPageUrlTextBox.Text);
							}
						}, TaskScheduler.FromCurrentSynchronizationContext ());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show ($"Navigation error: {ex.Message}");
			}
		}

		private void appPageUrlTextBox_TextChanged (object sender, EventArgs e)
		{
			this.navigateButton.Enabled = !string.IsNullOrWhiteSpace (this.appPageUrlTextBox.Text);
		}

		private void WebUIPageStudioScreen_Resize (object sender, EventArgs e)
		{
			try
			{
				var browserWidthPercentage = 85;
				var interControlGap = 15;
				var doubleGap = interControlGap * 2;

				var width = this.Width;
				var height = this.Height;

				this.appPageUrlLabel.Top = interControlGap;
				this.appPageUrlLabel.Left = interControlGap;

				this.navigateButton.Top = interControlGap;
				this.navigateButton.Left = width - this.navigateButton.Size.Width - doubleGap;

				this.appPageUrlTextBox.Top = interControlGap;
				this.appPageUrlTextBox.Left = this.appPageUrlLabel.Right + interControlGap;
				this.appPageUrlTextBox.Width = this.navigateButton.Left - this.appPageUrlTextBox.Left - interControlGap;

				this.navigateButton.Size = new Size (this.navigateButton.Width, this.appPageUrlTextBox.Height);

				this.appPageWebView.Top = this.appPageUrlTextBox.Bottom + interControlGap;
				this.appPageWebView.Left = interControlGap;
				this.appPageWebView.Width = width * browserWidthPercentage / 100;
				this.appPageWebView.Height = height - this.appPageWebView.Top - this.statusStrip.Height - 5 * interControlGap + 5;

				this.selectedElementsGroupBox.Top = this.appPageWebView.Top;
				this.selectedElementsGroupBox.Left = this.appPageWebView.Right + interControlGap;
				this.selectedElementsGroupBox.Width = width - this.selectedElementsGroupBox.Left - doubleGap;
				this.selectedElementsGroupBox.Height = this.appPageWebView.Height;

				this.buildPageCodeButton.Location = new Point (interControlGap, this.selectedElementsGroupBox.Height - interControlGap * 5 + 5);
				this.buildPageCodeButton.Width = this.selectedElementsGroupBox.Width - doubleGap;

				this.tagRenderAreaPictureBox.Location  = new Point (interControlGap, doubleGap);
				this.tagRenderAreaPictureBox.Width = this.selectedElementsGroupBox.Width - doubleGap;

				this.selectedElementsListBox.Location = new Point (interControlGap, this.tagRenderAreaPictureBox.Bottom + interControlGap);
				this.selectedElementsListBox.Width = this.selectedElementsGroupBox.Width - doubleGap;
				this.selectedElementsListBox.Height = this.selectedElementsGroupBox.Height - this.tagRenderAreaPictureBox.Height - this.buildPageCodeButton.Height - doubleGap * 2;

				this.pageIsLoadingLabel.Left = (this.appPageWebView.Width - this.pageIsLoadingLabel.Width) / 2;
				this.pageIsLoadingLabel.Top = (this.appPageWebView.Height - this.pageIsLoadingLabel.Height) / 2;
			}
			catch { }
		}

		private void StartFresh ()
		{
			this.receivedElementInfo = null;
			this.selectedElements.Clear ();
			this.buildPageCodeButton.Enabled = false;
			this.tagRenderAreaPictureBox.Image?.Dispose ();
			this.tagRenderAreaPictureBox.Image = Image.FromStream (new MemoryStream (StudioResources.PreviewImage));
			this.toolStripTagsCountLabel.Text = $"{ this.selectedElements.Count} elements selected";
		}

		private void ShowElementPreviw ()
		{
			if (this.selectedElementsListBox.Items.Count > 0)
			{
				var selectedItem = this.selectedElementsListBox.SelectedItem as HtmlTagInfo;
				if (selectedItem != null)
				{
					this.tagRenderAreaPictureBox.Image = selectedItem.TagRenderImage;
				}
			}
			else
			{
				this.tagRenderAreaPictureBox.Image = Image.FromStream (new MemoryStream (StudioResources.PreviewImage));
			}
		}

		private void buildPageCodeButton_Click (object sender, EventArgs e)
		{
			if (this.selectedElements.Count == 0)
			{
				MessageBox.Show ("Please select at least one element to build the page code.", "No Elements Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			WebPageModelDetailsScreen.DefinedInstance.SetSuggestedPageName (this.suggestedPageModelName);
			WebPageModelDetailsScreen.DefinedInstance.LoadSelectedElements (this.selectedElements);
			WebPageModelDetailsScreen.DefinedInstance.ShowDialog (this);
		}

		private void selectedElementsListBox_KeyDown (object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				if (selectedElementsListBox.SelectedItem != null)
				{
					// Get the selected element
					var selectedElement = selectedElementsListBox.SelectedItem as HtmlTagInfo;

					DialogResult result = MessageBox.Show (
						$"Are you sure you want to delete the element '{selectedElement?.Description}'?",
						"Confirm Deletion",
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Question
					);

					if (result == DialogResult.Yes)
					{
						// Since the selectedElementsListBox is bound to the selectedElements list, it would automatically remove this entry too.
						this.selectedElements.Remove (selectedElement);

						this.toolStripTagsCountLabel.Text = $"{this.selectedElements.Count} elements selected";

						// If there are no elements left, reset the preview image and disable the build button
						this.ShowElementPreviw ();
					}

					e.Handled = true;
					e.SuppressKeyPress = true;
				}
				else
				{
					MessageBox.Show ("Please select an element to delete.", "No element selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}

		private void pageLoadingTimer_Tick (object sender, EventArgs e)
		{
			this.pageIsLoadingLabel.Visible = !this.pageIsLoadingLabel.Visible;
		}
	}
}