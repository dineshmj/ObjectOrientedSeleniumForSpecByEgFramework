using System.ComponentModel;
using System.Text;
using System.Text.Json;

using Microsoft.Web.WebView2.Core;

using OOSelenium.WebUIPageStudio.Entities;
using OOSelenium.WebUIPageStudio.Resources;

namespace OOSelenium.WebUIPageStudio
{
	public partial class WebUIPageStudioScreen
		: Form
	{
		private HtmlTagInfo? receivedElementInfo;
		private BindingList<HtmlTagInfo> selectedElements = [];
		private float displayScalingFactor;

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
                    function getXPath(element) {
                        if (element.id !== '') {
                            return 'id(""' + element.id + '"")';
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

                    document.addEventListener('contextmenu', function(e) {
                        const el = e.target;
						const parent = el.parentElement;
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
                            " + nameof (HtmlTagInfo.XPath) + @": getXPath(el),
							" + nameof (HtmlTagInfo.ParentTag) + @": parent ? parent.tagName : null,
					        " + nameof (HtmlTagInfo.ParentHasMultiple) + @": parent ? parent.hasAttribute('multiple') : false,
							" + nameof (HtmlTagInfo.ParentXPath) + @": getXPath(parent),
							" + nameof (HtmlTagInfo.ParentName) + @": parent ? parent.getAttribute('name') : null,
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
					var elementAlreadyAdded = this.selectedElements.Any (x => x.XPath == this.receivedElementInfo.XPath);

					// Add an item to the context menu to add/remove the element
					customContextMenu.Items.Add ($"{(elementAlreadyAdded ? "Remove" : "Add")} {this.receivedElementInfo} element {(elementAlreadyAdded ? "from" : "to")} list", null, (s, args) =>
					{
						if (elementAlreadyAdded)
						{
							var elementToRemove = this.selectedElements.FirstOrDefault (x => x.XPath == this.receivedElementInfo.XPath);
							this.selectedElements.Remove (elementToRemove);
						}
						else
						{
							this.selectedElements.Add (this.receivedElementInfo);
						}

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

					// Calculate the crop area based on the received element's render area
					var cropArea = new Rectangle (
						(int) (receivedElementInfo.TagRenderArea.Left * this.displayScalingFactor),
						(int) (receivedElementInfo.TagRenderArea.Top * this.displayScalingFactor),
						(int) (receivedElementInfo.TagRenderArea.Width * this.displayScalingFactor),
						(int) (receivedElementInfo.TagRenderArea.Height * this.displayScalingFactor)
					);

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

			WebPageModelDetailsScreen.DefinedInstance.LoadSelectedElements (this.selectedElements);
			WebPageModelDetailsScreen.DefinedInstance.ShowDialog (this);

			var pageCodeBuilder = new StringBuilder ();
			var webPageModelTemplate = StudioResources.WebPageModelTemplate;
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