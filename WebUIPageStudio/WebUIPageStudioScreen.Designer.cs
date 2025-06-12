


using Microsoft.Web.WebView2.Core;

namespace OOSelenium.WebUIPageStudio
{
	partial class WebUIPageStudioScreen
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager (typeof (WebUIPageStudioScreen));
			appPageWebView = new Microsoft.Web.WebView2.WinForms.WebView2 ();
			appPageUrlLabel = new Label ();
			appPageUrlTextBox = new TextBox ();
			navigateButton = new Button ();
			((System.ComponentModel.ISupportInitialize) appPageWebView).BeginInit ();
			SuspendLayout ();
			// 
			// appPageWebView
			// 
			appPageWebView.AllowExternalDrop = true;
			appPageWebView.CreationProperties = null;
			appPageWebView.DefaultBackgroundColor = Color.White;
			appPageWebView.Location = new Point (4, 98);
			appPageWebView.Margin = new Padding (4, 5, 4, 5);
			appPageWebView.Name = "appPageWebView";
			appPageWebView.Size = new Size (2086, 912);
			appPageWebView.TabIndex = 0;
			appPageWebView.ZoomFactor = 1D;
			// 
			// appPageUrlLabel
			// 
			appPageUrlLabel.AutoSize = true;
			appPageUrlLabel.Location = new Point (27, 40);
			appPageUrlLabel.Margin = new Padding (4, 0, 4, 0);
			appPageUrlLabel.Name = "appPageUrlLabel";
			appPageUrlLabel.Size = new Size (185, 25);
			appPageUrlLabel.TabIndex = 1;
			appPageUrlLabel.Text = "Application Page URL:";
			appPageUrlLabel.TextAlign = ContentAlignment.MiddleRight;
			// 
			// appPageUrlTextBox
			// 
			appPageUrlTextBox.Font = new Font ("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point,  0);
			appPageUrlTextBox.Location = new Point (213, 35);
			appPageUrlTextBox.Margin = new Padding (4, 5, 4, 5);
			appPageUrlTextBox.Name = "appPageUrlTextBox";
			appPageUrlTextBox.Size = new Size (1805, 30);
			appPageUrlTextBox.TabIndex = 2;
			appPageUrlTextBox.Text = "https://www.cba.com.au";
			appPageUrlTextBox.TextChanged += appPageUrlTextBox_TextChanged;
			// 
			// navigateButton
			// 
			navigateButton.Location = new Point (2029, 33);
			navigateButton.Margin = new Padding (4, 5, 4, 5);
			navigateButton.Name = "navigateButton";
			navigateButton.Size = new Size (61, 40);
			navigateButton.TabIndex = 3;
			navigateButton.Text = "Go!";
			navigateButton.UseVisualStyleBackColor = true;
			navigateButton.Click += navigateButton_Click;
			// 
			// WebUIPageStudioScreen
			// 
			AutoScaleDimensions = new SizeF (10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size (2107, 1097);
			Controls.Add (navigateButton);
			Controls.Add (appPageUrlTextBox);
			Controls.Add (appPageUrlLabel);
			Controls.Add (appPageWebView);
			Icon = (Icon) resources.GetObject ("$this.Icon");
			Margin = new Padding (4, 5, 4, 5);
			Name = "WebUIPageStudioScreen";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Web UI Page Studio";
			WindowState = FormWindowState.Maximized;
			Load += WebUIPageStudioScreen_Load;
			Resize += WebUIPageStudioScreen_Resize;
			((System.ComponentModel.ISupportInitialize) appPageWebView).EndInit ();
			ResumeLayout (false);
			PerformLayout ();
		}

		#endregion

		private Microsoft.Web.WebView2.WinForms.WebView2 appPageWebView;
		private Label appPageUrlLabel;
		private TextBox appPageUrlTextBox;
		private Button navigateButton;
	}
}
