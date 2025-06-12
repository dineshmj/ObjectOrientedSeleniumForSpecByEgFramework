using Microsoft.Web.WebView2.Core;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace OOSelenium.WebUIPageStudio
{
	public partial class WebUIPageStudioScreen : Form
	{
		public WebUIPageStudioScreen ()
		{
			InitializeComponent ();
		}

		private async void WebUIPageStudioScreen_Load (object sender, EventArgs e)
		{
			await this.appPageWebView.EnsureCoreWebView2Async ();

			this.appPageWebView.CoreWebView2.ContextMenuRequested
				+= this.appPageWebView_ContextMenuRequested;

			this.appPageWebView.CoreWebView2.WebMessageReceived
				+= this.appPageWebView_WebMessageReceived;
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
			catch
			{
			}
		}

		private async void appPageWebView_ContextMenuRequested (object? sender, CoreWebView2ContextMenuRequestedEventArgs e)
		{
			MessageBox.Show ("Context menu requested. Right-click functionality is disabled in this application.", "Context Menu", MessageBoxButtons.OK, MessageBoxIcon.Information);

			e.Handled = true;
			await ProcessContextMenuRequestAsync (e.Location);
		}

		private async Task ProcessContextMenuRequestAsync (Point rightClickPoint)
		{
			// Prevent the default context menu from showing
			// JavaScript code to get information about the right-clicked element
			string script = @"
            const targetElement = document.elementFromPoint(" + rightClickPoint.X + @", " + rightClickPoint.Y + @");
            let elementInfo = {}; // This is now a literal JavaScript object literal
            if (targetElement) {
                if (targetElement.id) {
                    elementInfo.id = targetElement.id;
                }
                if (targetElement.name) {
                    elementInfo.name = targetElement.name;
                }
                if (targetElement.className) {
                    elementInfo.className = targetElement.className;
                }
                if (targetElement.tagName) {
                    elementInfo.tagName = targetElement.tagName;
                    if (targetElement.tagName.toUpperCase() === 'INPUT' && targetElement.type) {
                        elementInfo.inputType = targetElement.type.toLowerCase();
                    }
                }
                if (targetElement.innerText && targetElement.innerText.trim() !== '') {
                    elementInfo.text = targetElement.innerText.trim();
                } else if (targetElement.value && targetElement.value.trim() !== '' && (targetElement.tagName.toUpperCase() === 'INPUT' || targetElement.tagName.toUpperCase() === 'TEXTAREA')) {
                    elementInfo.text = targetElement.value.trim();
                }

                function getXPath(element) {
                    let path = '';
                    if (element.id !== '') {
                        // Using JavaScript Template Literals (backticks ` `) for clean string building.
                        // Inside a C# verbatim string, ` is a literal backtick.
                        // So, `id(""${element.id}"")` in JS is written as `id(""` + ${element.id} + `"")` in C#
                        // However, C# verbatim strings: `""` -> `""`
                        // So, if we want the JS string `id(""${element.id}"")`
                        // We write it as: `id(""${element.id}"")`
                        path = `id(""${element.id}"")`; // This will correctly result in JS `id(""${element.id}"")`
                    } else if (element === document.body) {
                        path = element.tagName.toLowerCase();
                    } else {
                        var ix = 0;
                        var siblings = element.parentNode.childNodes;
                        for (var i = 0; i < siblings.length; i++) {
                            var sibling = siblings[i];
                            if (sibling === element) {
                                path = getXPath(element.parentNode) + '/' + element.tagName.toLowerCase() + '[' + (ix + 1) + ']';
                                break;
                            }
                            if (sibling.nodeType === 1 && sibling.tagName === element.tagName) {
                                ix++;
                            }
                        }
                    }
                    return path;
                }
                elementInfo.xpath = getXPath(targetElement);
            } else {
                elementInfo.error = 'No identifiable element at cursor position.';
                elementInfo.tagName = 'UNKNOWN';
            }
            window.chrome.webview.postMessage(JSON.stringify(elementInfo));
        ";

			try
			{
				await this.appPageWebView.CoreWebView2.ExecuteScriptAsync (script);
			}
			catch (Exception ex)
			{
				MessageBox.Show ($"Error executing script: {ex.Message}");
			}
		}

		private void appPageWebView_WebMessageReceived (object sender, CoreWebView2WebMessageReceivedEventArgs e)
		{
			MessageBox.Show ("WebMessageReceived event triggered. Right-click functionality is disabled in this application.", "Web Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

			string jsonString = Regex.Unescape (e.WebMessageAsJson.Trim ('"'));

			try
			{
				var elementData = JsonSerializer.Deserialize<Dictionary<string, string>> (jsonString);
				if (elementData != null)
				{
					string elementType = elementData.GetValueOrDefault ("tagName");
					string inputType = elementData.GetValueOrDefault ("inputType");
					string id = elementData.GetValueOrDefault ("id");
					string name = elementData.GetValueOrDefault ("name");
					string className = elementData.GetValueOrDefault ("className");
					string tagText = elementData.GetValueOrDefault ("text");
					string xpath = elementData.GetValueOrDefault ("xpath");

					MessageBox.Show ($"Select element's detaiils:\r\n\r\nElement type: {elementType}\r\nInput type: {inputType}\r\nid: {id}\r\nname: {name}\r\nCSS class name: {className}\r\nTag text: {tagText}\r\nX-path: {xpath}", "Element details", MessageBoxButtons.OK, MessageBoxIcon.Information);

				}
			}
			catch (JsonException ex)
			{
				Console.WriteLine ($"Error deserializing web message: {ex.Message}");
			}
		}


		private void appPageUrlTextBox_TextChanged (object sender, EventArgs e)
		{
			this.navigateButton.Enabled
				= !(
					string.IsNullOrEmpty (this.appPageUrlTextBox.Text)
					|| string.IsNullOrWhiteSpace (this.appPageUrlTextBox.Text));
		}

		private void navigateButton_Click (object sender, EventArgs e)
		{
			try
			{
				this.appPageWebView
					.EnsureCoreWebView2Async (null)
					.ContinueWith (t =>
					{
						if (t.Status == TaskStatus.RanToCompletion)
						{
							this.appPageWebView.CoreWebView2.Navigate ("about:blank");
							this.appPageWebView.CoreWebView2.Navigate (this.appPageUrlTextBox.Text);
						}
					},
					TaskScheduler.FromCurrentSynchronizationContext ());
			}
			catch (Exception ex)
			{
				MessageBox.Show ($"Error occurred while navigating to the specified URL.\r\n\r\nError description: {ex.Message}", "Error in Navigation", MessageBoxButtons.OK, MessageBoxIcon.Error);

				this.appPageUrlTextBox.Focus ();
			}
		}
	}
}