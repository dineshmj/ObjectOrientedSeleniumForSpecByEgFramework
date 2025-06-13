using Microsoft.Web.WebView2.Core;
using OOSelenium.WebUIPageStudio.Entities;
using OOSelenium.WebUIPageStudio.Resources;
using System.ComponentModel;
using System.Text.Json;

namespace OOSelenium.WebUIPageStudio
{
	public partial class WebUIPageStudioScreen : Form
	{
		private HtmlTagInfo? receivedElementInfo;
		private BindingList<HtmlTagInfo> selectedElements = new BindingList<HtmlTagInfo> ();
		private float scalingFactor;

		public WebUIPageStudioScreen ()
		{
			InitializeComponent ();
		}

		private async void WebUIPageStudioScreen_Load (object sender, EventArgs e)
		{
			this.scalingFactor = DpiHelper.GetScalingFactor (this);

			await this.appPageWebView.EnsureCoreWebView2Async ();

			this.selectedElementsListBox.DataSource = this.selectedElements;
			this.selectedElementsListBox.DisplayMember = nameof (HtmlTagInfo.Description);

			this.appPageWebView.CoreWebView2.NavigationStarting += CoreWebView2_NavigationStarting;
			this.appPageWebView.CoreWebView2.NavigationCompleted += this.CoreWebView2_NavigationCompleted;

			this.appPageWebView.CoreWebView2.ContextMenuRequested += this.appPageWebView_ContextMenuRequested;
			this.appPageWebView.CoreWebView2.WebMessageReceived += this.appPageWebView_WebMessageReceived;

			this.StartFresh ();
		}

		private void CoreWebView2_NavigationStarting (object? sender, CoreWebView2NavigationStartingEventArgs e)
		{
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

			// The user wants to navigate to a new page, so we clear the current selections.
			this.StartFresh ();
		}

		private async void CoreWebView2_NavigationCompleted (object? sender, CoreWebView2NavigationCompletedEventArgs e)
		{
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
						const rect = el.getBoundingClientRect();

                        const details = {
                            Tag: el.tagName,
							Text: el.innerText,
                            Id: el.id,
                            CssClass: el.className,
                            Name: el.getAttribute('name'),
                            Source: el.getAttribute('src'),
                            LinkURL: el.getAttribute('href'),
                            Type: el.getAttribute('type'),
                            XPath: getXPath(el),
							TagRenderArea: {
								Top: rect.top,
								Left: rect.left,
								Width: rect.width,
								Height: rect.height,
								ClickX: e.clientX,
								ClickY: e.clientY
							}
                        };
                        window.chrome.webview.postMessage(details);
                    });
                })();
            ";

			await this.appPageWebView.ExecuteScriptAsync (js);

			try
			{
				this.appPageUrlTextBox.Text = this.appPageWebView.Source.ToString ();
			}
			catch
			{
			}
		}

		private void appPageWebView_ContextMenuRequested (object? sender, CoreWebView2ContextMenuRequestedEventArgs e)
		{
			e.Handled = true;

			if (this.receivedElementInfo != null)
			{
				var elementAlreadyAdded = this.selectedElements.Any (x => x.XPath == this.receivedElementInfo.XPath);

				var menu = new ContextMenuStrip ();
				menu.Items.Add ($"{(elementAlreadyAdded ? "Remove" : "Add")} {this.receivedElementInfo} element {(elementAlreadyAdded ? "from" : "to")} list", null, (s, args) =>
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

					this.selectedElementsListBox.SelectedIndex = this.selectedElementsListBox.Items.Count - 1;
					this.ShowElementPreviw ();

					this.receivedElementInfo = null;
				});

				var point = Cursor.Position;
				menu.Show (point);
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

				this.receivedElementInfo = JsonSerializer.Deserialize<HtmlTagInfo> (e.WebMessageAsJson, options);

				if (this.receivedElementInfo != null)
				{
					using var stream = new MemoryStream ();
					await this.appPageWebView.CoreWebView2.CapturePreviewAsync (CoreWebView2CapturePreviewImageFormat.Png, stream);
					stream.Position = 0;

					using var fullBitmap = new Bitmap (stream);

					if (this.receivedElementInfo == null)
					{
						this.receivedElementInfo = JsonSerializer.Deserialize<HtmlTagInfo> (e.WebMessageAsJson, options);
					}

					var cropArea = new Rectangle (
						(int) (receivedElementInfo.TagRenderArea.Left * this.scalingFactor),
						(int) (receivedElementInfo.TagRenderArea.Top * this.scalingFactor),
						(int) (receivedElementInfo.TagRenderArea.Width * this.scalingFactor),
						(int) (receivedElementInfo.TagRenderArea.Height * this.scalingFactor)
					);

					// Ensure crop area is within bounds
					cropArea.Intersect (new Rectangle (Point.Empty, fullBitmap.Size));
					this.receivedElementInfo.TagRender = fullBitmap.Clone (cropArea, fullBitmap.PixelFormat);
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
			if (this.appPageUrlTextBox.Text == this.appPageWebView?.Source?.ToString ())
			{
				MessageBox.Show ("You're already on this page!", "Navigation not required", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

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

				this.tagRenderAreaPictureBox.Location  = new Point (interControlGap, doubleGap);
				this.tagRenderAreaPictureBox.Width = this.selectedElementsGroupBox.Width - doubleGap;

				this.selectedElementsListBox.Location = new Point (interControlGap, this.tagRenderAreaPictureBox.Bottom + interControlGap);
				this.selectedElementsListBox.Width = this.selectedElementsGroupBox.Width - doubleGap;
				this.selectedElementsListBox.Height = this.selectedElementsGroupBox.Height - doubleGap;
			}
			catch { }
		}

		private void StartFresh ()
		{
			this.receivedElementInfo = null;
			this.selectedElements.Clear ();
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
					this.tagRenderAreaPictureBox.Image = selectedItem.TagRender;
				}
			}
		}

	}
}