namespace OOSelenium.WebUIPageStudio
{
	partial class UIControlHtmlTagMapperControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			components = new System.ComponentModel.Container ();
			nOfTotalLabel = new Label ();
			htmlTagNameLabel = new Label ();
			htmlTagNameValueLabel = new Label ();
			previewPictureBox = new PictureBox ();
			mappedControlNameValueLabel = new Label ();
			mappedControlNameLabel = new Label ();
			backgroundLabel = new Label ();
			pageModelPropertyNameLabel = new Label ();
			pageModelPropertyNameTextBox = new TextBox ();
			nameOkPictureBox = new PictureBox ();
			nameOkLabel = new Label ();
			whyNameNotOkTooltip = new ToolTip (components);
			((System.ComponentModel.ISupportInitialize) previewPictureBox).BeginInit ();
			((System.ComponentModel.ISupportInitialize) nameOkPictureBox).BeginInit ();
			SuspendLayout ();
			// 
			// nOfTotalLabel
			// 
			nOfTotalLabel.Font = new Font ("Segoe UI Black", 10F, FontStyle.Bold, GraphicsUnit.Point,  0);
			nOfTotalLabel.Location = new Point (10, 10);
			nOfTotalLabel.Name = "nOfTotalLabel";
			nOfTotalLabel.Size = new Size (120, 100);
			nOfTotalLabel.TabIndex = 0;
			nOfTotalLabel.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// htmlTagNameLabel
			// 
			htmlTagNameLabel.BackColor = Color.LightCyan;
			htmlTagNameLabel.Font = new Font ("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point,  0);
			htmlTagNameLabel.Location = new Point (145, 20);
			htmlTagNameLabel.Name = "htmlTagNameLabel";
			htmlTagNameLabel.Size = new Size (320, 36);
			htmlTagNameLabel.TabIndex = 1;
			htmlTagNameLabel.Text = "HTML Tag Name";
			// 
			// htmlTagNameValueLabel
			// 
			htmlTagNameValueLabel.BorderStyle = BorderStyle.FixedSingle;
			htmlTagNameValueLabel.Font = new Font ("Consolas", 10F, FontStyle.Bold, GraphicsUnit.Point,  0);
			htmlTagNameValueLabel.Location = new Point (150, 60);
			htmlTagNameValueLabel.Name = "htmlTagNameValueLabel";
			htmlTagNameValueLabel.Size = new Size (320, 36);
			htmlTagNameValueLabel.TabIndex = 2;
			htmlTagNameValueLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// previewPictureBox
			// 
			previewPictureBox.BorderStyle = BorderStyle.FixedSingle;
			previewPictureBox.Location = new Point (1260, 10);
			previewPictureBox.Name = "previewPictureBox";
			previewPictureBox.Size = new Size (300, 100);
			previewPictureBox.TabIndex = 3;
			previewPictureBox.TabStop = false;
			// 
			// mappedControlNameValueLabel
			// 
			mappedControlNameValueLabel.BackColor = Color.Black;
			mappedControlNameValueLabel.BorderStyle = BorderStyle.FixedSingle;
			mappedControlNameValueLabel.Font = new Font ("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point,  0);
			mappedControlNameValueLabel.ForeColor = Color.Lime;
			mappedControlNameValueLabel.Location = new Point (480, 60);
			mappedControlNameValueLabel.Name = "mappedControlNameValueLabel";
			mappedControlNameValueLabel.Size = new Size (320, 36);
			mappedControlNameValueLabel.TabIndex = 2;
			mappedControlNameValueLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// mappedControlNameLabel
			// 
			mappedControlNameLabel.BackColor = Color.LightCyan;
			mappedControlNameLabel.Font = new Font ("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point,  0);
			mappedControlNameLabel.Location = new Point (475, 20);
			mappedControlNameLabel.Name = "mappedControlNameLabel";
			mappedControlNameLabel.Size = new Size (320, 36);
			mappedControlNameLabel.TabIndex = 1;
			mappedControlNameLabel.Text = "Mapped OOSF Control";
			// 
			// backgroundLabel
			// 
			backgroundLabel.BackColor = Color.LightCyan;
			backgroundLabel.BorderStyle = BorderStyle.Fixed3D;
			backgroundLabel.Location = new Point (140, 10);
			backgroundLabel.Name = "backgroundLabel";
			backgroundLabel.Size = new Size (1110, 100);
			backgroundLabel.TabIndex = 4;
			// 
			// pageModelPropertyNameLabel
			// 
			pageModelPropertyNameLabel.BackColor = Color.LightCyan;
			pageModelPropertyNameLabel.Font = new Font ("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point,  0);
			pageModelPropertyNameLabel.Location = new Point (805, 20);
			pageModelPropertyNameLabel.Name = "pageModelPropertyNameLabel";
			pageModelPropertyNameLabel.Size = new Size (320, 36);
			pageModelPropertyNameLabel.TabIndex = 1;
			pageModelPropertyNameLabel.Text = "Enter Page Model Property Name here";
			// 
			// pageModelPropertyNameTextBox
			// 
			pageModelPropertyNameTextBox.BackColor = Color.FromArgb (  64,   64,   64);
			pageModelPropertyNameTextBox.BorderStyle = BorderStyle.FixedSingle;
			pageModelPropertyNameTextBox.Font = new Font ("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point,  0);
			pageModelPropertyNameTextBox.ForeColor = Color.White;
			pageModelPropertyNameTextBox.Location = new Point (810, 60);
			pageModelPropertyNameTextBox.Name = "pageModelPropertyNameTextBox";
			pageModelPropertyNameTextBox.Size = new Size (320, 36);
			pageModelPropertyNameTextBox.TabIndex = 5;
			pageModelPropertyNameTextBox.TextChanged += pageModelPropertyNameTextBox_TextChanged;
			// 
			// nameOkPictureBox
			// 
			nameOkPictureBox.Image = Resources.StudioResources.Ok;
			nameOkPictureBox.Location = new Point (1140, 60);
			nameOkPictureBox.Name = "nameOkPictureBox";
			nameOkPictureBox.Size = new Size (36, 36);
			nameOkPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			nameOkPictureBox.TabIndex = 6;
			nameOkPictureBox.TabStop = false;
			// 
			// nameOkLabel
			// 
			nameOkLabel.BackColor = Color.LightCyan;
			nameOkLabel.Font = new Font ("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point,  0);
			nameOkLabel.Location = new Point (1135, 20);
			nameOkLabel.Name = "nameOkLabel";
			nameOkLabel.Size = new Size (100, 36);
			nameOkLabel.TabIndex = 1;
			nameOkLabel.Text = "Name OK?";
			// 
			// whyNameNotOkTooltip
			// 
			whyNameNotOkTooltip.ToolTipIcon = ToolTipIcon.Warning;
			whyNameNotOkTooltip.ToolTipTitle = "Property Name must end with `Span`";
			// 
			// UIControlHtmlTagMapperControl
			// 
			AutoScaleDimensions = new SizeF (10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add (nameOkPictureBox);
			Controls.Add (pageModelPropertyNameTextBox);
			Controls.Add (previewPictureBox);
			Controls.Add (mappedControlNameValueLabel);
			Controls.Add (htmlTagNameValueLabel);
			Controls.Add (nameOkLabel);
			Controls.Add (pageModelPropertyNameLabel);
			Controls.Add (mappedControlNameLabel);
			Controls.Add (htmlTagNameLabel);
			Controls.Add (nOfTotalLabel);
			Controls.Add (backgroundLabel);
			Name = "UIControlHtmlTagMapperControl";
			Size = new Size (1570, 120);
			((System.ComponentModel.ISupportInitialize) previewPictureBox).EndInit ();
			((System.ComponentModel.ISupportInitialize) nameOkPictureBox).EndInit ();
			ResumeLayout (false);
			PerformLayout ();
		}

		#endregion

		private Label nOfTotalLabel;
		private Label htmlTagNameLabel;
		private Label htmlTagNameValueLabel;
		private PictureBox previewPictureBox;
		private Label mappedControlNameValueLabel;
		private Label mappedControlNameLabel;
		private Label backgroundLabel;
		private Label pageModelPropertyNameLabel;
		private TextBox pageModelPropertyNameTextBox;
		private PictureBox nameOkPictureBox;
		private Label nameOkLabel;
		private ToolTip whyNameNotOkTooltip;
	}
}
