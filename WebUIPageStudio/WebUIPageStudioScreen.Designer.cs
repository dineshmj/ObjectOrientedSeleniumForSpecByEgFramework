


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
			components = new System.ComponentModel.Container ();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager (typeof (WebUIPageStudioScreen));
			appPageWebView = new Microsoft.Web.WebView2.WinForms.WebView2 ();
			appPageUrlLabel = new Label ();
			appPageUrlTextBox = new TextBox ();
			navigateButton = new Button ();
			selectedElementsGroupBox = new GroupBox ();
			buildPageCodeButton = new Button ();
			tagRenderAreaPictureBox = new PictureBox ();
			selectedElementsListBox = new ListBox ();
			statusStrip = new StatusStrip ();
			toolStripTagsCountLabel = new ToolStripStatusLabel ();
			pageLoadingTimer = new System.Windows.Forms.Timer (components);
			pageIsLoadingLabel = new Label ();
			((System.ComponentModel.ISupportInitialize) appPageWebView).BeginInit ();
			selectedElementsGroupBox.SuspendLayout ();
			((System.ComponentModel.ISupportInitialize) tagRenderAreaPictureBox).BeginInit ();
			statusStrip.SuspendLayout ();
			SuspendLayout ();
			// 
			// appPageWebView
			// 
			appPageWebView.AllowExternalDrop = true;
			appPageWebView.BackColor = Color.White;
			appPageWebView.CreationProperties = null;
			appPageWebView.DefaultBackgroundColor = Color.White;
			appPageWebView.Location = new Point (13, 86);
			appPageWebView.Margin = new Padding (4, 5, 4, 5);
			appPageWebView.Name = "appPageWebView";
			appPageWebView.Size = new Size (1711, 1122);
			appPageWebView.TabIndex = 0;
			appPageWebView.ZoomFactor = 1D;
			// 
			// appPageUrlLabel
			// 
			appPageUrlLabel.Location = new Point (13, 28);
			appPageUrlLabel.Margin = new Padding (4, 0, 4, 0);
			appPageUrlLabel.Name = "appPageUrlLabel";
			appPageUrlLabel.Size = new Size (144, 30);
			appPageUrlLabel.TabIndex = 1;
			appPageUrlLabel.Text = "Application URL:";
			// 
			// appPageUrlTextBox
			// 
			appPageUrlTextBox.Font = new Font ("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point,  0);
			appPageUrlTextBox.Location = new Point (165, 28);
			appPageUrlTextBox.Margin = new Padding (4, 5, 4, 5);
			appPageUrlTextBox.Name = "appPageUrlTextBox";
			appPageUrlTextBox.Size = new Size (1853, 30);
			appPageUrlTextBox.TabIndex = 2;
			appPageUrlTextBox.Text = "https://www.amazon.in";
			appPageUrlTextBox.TextChanged += appPageUrlTextBox_TextChanged;
			// 
			// navigateButton
			// 
			navigateButton.Location = new Point (2029, 28);
			navigateButton.Margin = new Padding (4, 5, 4, 5);
			navigateButton.Name = "navigateButton";
			navigateButton.Size = new Size (58, 30);
			navigateButton.TabIndex = 3;
			navigateButton.Text = "Go!";
			navigateButton.UseVisualStyleBackColor = true;
			navigateButton.Click += navigateButton_Click;
			// 
			// selectedElementsGroupBox
			// 
			selectedElementsGroupBox.Controls.Add (buildPageCodeButton);
			selectedElementsGroupBox.Controls.Add (tagRenderAreaPictureBox);
			selectedElementsGroupBox.Controls.Add (selectedElementsListBox);
			selectedElementsGroupBox.Location = new Point (1730, 74);
			selectedElementsGroupBox.Name = "selectedElementsGroupBox";
			selectedElementsGroupBox.Size = new Size (357, 1123);
			selectedElementsGroupBox.TabIndex = 4;
			selectedElementsGroupBox.TabStop = false;
			selectedElementsGroupBox.Text = "Selected HTML Tags";
			// 
			// buildPageCodeButton
			// 
			buildPageCodeButton.Enabled = false;
			buildPageCodeButton.Font = new Font ("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point,  0);
			buildPageCodeButton.ForeColor = Color.Green;
			buildPageCodeButton.Location = new Point (12, 1030);
			buildPageCodeButton.Name = "buildPageCodeButton";
			buildPageCodeButton.Size = new Size (339, 57);
			buildPageCodeButton.TabIndex = 2;
			buildPageCodeButton.Text = "Proceed to Build Page Code";
			buildPageCodeButton.UseVisualStyleBackColor = true;
			buildPageCodeButton.Click += buildPageCodeButton_Click;
			// 
			// tagRenderAreaPictureBox
			// 
			tagRenderAreaPictureBox.BorderStyle = BorderStyle.Fixed3D;
			tagRenderAreaPictureBox.Location = new Point (10, 30);
			tagRenderAreaPictureBox.Name = "tagRenderAreaPictureBox";
			tagRenderAreaPictureBox.Size = new Size (341, 121);
			tagRenderAreaPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
			tagRenderAreaPictureBox.TabIndex = 1;
			tagRenderAreaPictureBox.TabStop = false;
			// 
			// selectedElementsListBox
			// 
			selectedElementsListBox.FormattingEnabled = true;
			selectedElementsListBox.ItemHeight = 25;
			selectedElementsListBox.Location = new Point (8, 157);
			selectedElementsListBox.Name = "selectedElementsListBox";
			selectedElementsListBox.Size = new Size (343, 854);
			selectedElementsListBox.TabIndex = 0;
			selectedElementsListBox.SelectedIndexChanged += selectedElementsListBox_SelectedIndexChanged;
			selectedElementsListBox.KeyDown += selectedElementsListBox_KeyDown;
			// 
			// statusStrip
			// 
			statusStrip.ImageScalingSize = new Size (24, 24);
			statusStrip.Items.AddRange (new ToolStripItem [] { toolStripTagsCountLabel });
			statusStrip.Location = new Point (0, 1174);
			statusStrip.Name = "statusStrip";
			statusStrip.Size = new Size (2100, 32);
			statusStrip.TabIndex = 5;
			statusStrip.Text = "statusStrip1";
			// 
			// toolStripTagsCountLabel
			// 
			toolStripTagsCountLabel.Name = "toolStripTagsCountLabel";
			toolStripTagsCountLabel.Size = new Size (190, 25);
			toolStripTagsCountLabel.Text = "Selected Tags Count: 0";
			// 
			// pageLoadingTimer
			// 
			pageLoadingTimer.Interval = 300;
			pageLoadingTimer.Tick += pageLoadingTimer_Tick;
			// 
			// pageIsLoadingLabel
			// 
			pageIsLoadingLabel.AutoSize = true;
			pageIsLoadingLabel.BackColor = Color.Transparent;
			pageIsLoadingLabel.Font = new Font ("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point,  0);
			pageIsLoadingLabel.ForeColor = Color.Maroon;
			pageIsLoadingLabel.Location = new Point (33, 108);
			pageIsLoadingLabel.Name = "pageIsLoadingLabel";
			pageIsLoadingLabel.Size = new Size (556, 54);
			pageIsLoadingLabel.TabIndex = 6;
			pageIsLoadingLabel.Text = "Page is loading. Please wait!";
			pageIsLoadingLabel.Visible = false;
			// 
			// WebUIPageStudioScreen
			// 
			AutoScaleDimensions = new SizeF (10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.Control;
			ClientSize = new Size (2100, 1206);
			Controls.Add (pageIsLoadingLabel);
			Controls.Add (statusStrip);
			Controls.Add (selectedElementsGroupBox);
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
			selectedElementsGroupBox.ResumeLayout (false);
			((System.ComponentModel.ISupportInitialize) tagRenderAreaPictureBox).EndInit ();
			statusStrip.ResumeLayout (false);
			statusStrip.PerformLayout ();
			ResumeLayout (false);
			PerformLayout ();
		}

		#endregion

		private Microsoft.Web.WebView2.WinForms.WebView2 appPageWebView;
		private Label appPageUrlLabel;
		private TextBox appPageUrlTextBox;
		private Button navigateButton;
		private GroupBox selectedElementsGroupBox;
		private ListBox selectedElementsListBox;
		private StatusStrip statusStrip;
		private ToolStripStatusLabel toolStripTagsCountLabel;
		private PictureBox tagRenderAreaPictureBox;
		private Button buildPageCodeButton;
		private System.Windows.Forms.Timer pageLoadingTimer;
		private Label pageIsLoadingLabel;
	}
}
