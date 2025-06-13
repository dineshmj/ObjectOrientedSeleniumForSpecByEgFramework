using Microsoft.Web.WebView2.Core;
using OOSelenium.WebUIPageStudio.Entities;
using System.Text.Json;

namespace OOSelenium.WebUIPageStudio
{
	public partial class WebUIPageStudioScreen : Form
	{
		private ElementInfo? receivedElementInfo;
		private List<ElementInfo> selectedElements = new List<ElementInfo> ();

		public WebUIPageStudioScreen ()
		{
			InitializeComponent ();
		}

		private async void WebUIPageStudioScreen_Load (object sender, EventArgs e)
		{
			await this.appPageWebView.EnsureCoreWebView2Async ();

			this.appPageWebView.CoreWebView2.WebMessageReceived += this.appPageWebView_WebMessageReceived;
			this.appPageWebView.CoreWebView2.ContextMenuRequested += this.appPageWebView_ContextMenuRequested;
			this.appPageWebView.CoreWebView2.NavigationCompleted += this.CoreWebView2_NavigationCompleted;
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
                        const details = {
                            Tag: el.tagName,
                            Id: el.id,
                            CssClass: el.className,
                            Name: el.getAttribute('name'),
                            Source: el.getAttribute('src'),
                            LinkURL: el.getAttribute('href'),
                            Type: el.getAttribute('type'),
                            XPath: getXPath(el)
                        };
                        window.chrome.webview.postMessage(details);
                    });
                })();
            ";

			await this.appPageWebView.ExecuteScriptAsync (js);
		}

		private void appPageWebView_ContextMenuRequested (object? sender, CoreWebView2ContextMenuRequestedEventArgs e)
		{
			e.Handled = true;

			if (this.receivedElementInfo != null)
			{
				var menu = new ContextMenuStrip ();
				menu.Items.Add ("Add this element to list", null, (s, args) =>
				{
					if (this.receivedElementInfo != null)
					{
						if (this.selectedElements.Any (x => x.XPath == this.receivedElementInfo.XPath))
						{
							MessageBox.Show ("Element already exists in the list");
							this.receivedElementInfo = null;
							return;
						}

						this.selectedElements.Add (this.receivedElementInfo);
						this.receivedElementInfo = null;
					}
					MessageBox.Show ("Element added");
				});

				var point = Cursor.Position;
				menu.Show (point);
			}
		}

		private void appPageWebView_WebMessageReceived (object sender, CoreWebView2WebMessageReceivedEventArgs e)
		{
			try
			{
				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				};

				this.receivedElementInfo = JsonSerializer.Deserialize<ElementInfo> (e.WebMessageAsJson, options);
			}
			catch
			{
				MessageBox.Show ($"Could not identify the HTML element on the page. Please try again.", "Element not identified", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void navigateButton_Click (object sender, EventArgs e)
		{
			try
			{
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

		private void WebUIPageStudioScreen_Resize (object sender, EventArgs e)
		{
			try
			{
				var screenWidth = this.Width;
				var screenHeight = this.Height;

				this.navigateButton.Left = screenWidth - this.navigateButton.Size.Width - 28;
				this.appPageUrlTextBox.Width = screenWidth - this.navigateButton.Size.Width - 42 - this.appPageUrlTextBox.Left;
				this.appPageWebView.Width = screenWidth - 28;
				this.appPageWebView.Height = screenHeight - 28;
			}
			catch { }
		}

		private void appPageUrlTextBox_TextChanged (object sender, EventArgs e)
		{
			this.navigateButton.Enabled = !string.IsNullOrWhiteSpace (this.appPageUrlTextBox.Text);
		}
	}
}
