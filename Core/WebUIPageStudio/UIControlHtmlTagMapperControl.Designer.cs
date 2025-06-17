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
			doNotInitializeInConstructorCheckBox = new CheckBox ();
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
			htmlTagNameLabel.Location = new Point (146, 20);
			htmlTagNameLabel.Name = "htmlTagNameLabel";
			htmlTagNameLabel.Size = new Size (400, 37);
			htmlTagNameLabel.TabIndex = 1;
			htmlTagNameLabel.Text = "HTML Tag Name";
			// 
			// htmlTagNameValueLabel
			// 
			htmlTagNameValueLabel.BorderStyle = BorderStyle.FixedSingle;
			htmlTagNameValueLabel.Font = new Font ("Consolas", 10F, FontStyle.Bold, GraphicsUnit.Point,  0);
			htmlTagNameValueLabel.Location = new Point (150, 60);
			htmlTagNameValueLabel.Name = "htmlTagNameValueLabel";
			htmlTagNameValueLabel.Size = new Size (401, 35);
			htmlTagNameValueLabel.TabIndex = 2;
			htmlTagNameValueLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// previewPictureBox
			// 
			previewPictureBox.BorderStyle = BorderStyle.FixedSingle;
			previewPictureBox.Location = new Point (1500, 10);
			previewPictureBox.Name = "previewPictureBox";
			previewPictureBox.Size = new Size (301, 135);
			previewPictureBox.TabIndex = 3;
			previewPictureBox.TabStop = false;
			// 
			// mappedControlNameValueLabel
			// 
			mappedControlNameValueLabel.BackColor = Color.Black;
			mappedControlNameValueLabel.BorderStyle = BorderStyle.FixedSingle;
			mappedControlNameValueLabel.Font = new Font ("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point,  0);
			mappedControlNameValueLabel.ForeColor = Color.Lime;
			mappedControlNameValueLabel.Location = new Point (560, 60);
			mappedControlNameValueLabel.Name = "mappedControlNameValueLabel";
			mappedControlNameValueLabel.Size = new Size (401, 35);
			mappedControlNameValueLabel.TabIndex = 2;
			mappedControlNameValueLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// mappedControlNameLabel
			// 
			mappedControlNameLabel.BackColor = Color.LightCyan;
			mappedControlNameLabel.Font = new Font ("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point,  0);
			mappedControlNameLabel.Location = new Point (560, 20);
			mappedControlNameLabel.Name = "mappedControlNameLabel";
			mappedControlNameLabel.Size = new Size (400, 37);
			mappedControlNameLabel.TabIndex = 1;
			mappedControlNameLabel.Text = "Mapped OOSF Control";
			// 
			// backgroundLabel
			// 
			backgroundLabel.BackColor = Color.LightCyan;
			backgroundLabel.BorderStyle = BorderStyle.Fixed3D;
			backgroundLabel.Location = new Point (140, 10);
			backgroundLabel.Name = "backgroundLabel";
			backgroundLabel.Size = new Size (1350, 133);
			backgroundLabel.TabIndex = 4;
			// 
			// pageModelPropertyNameLabel
			// 
			pageModelPropertyNameLabel.BackColor = Color.LightCyan;
			pageModelPropertyNameLabel.Font = new Font ("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point,  0);
			pageModelPropertyNameLabel.Location = new Point (970, 20);
			pageModelPropertyNameLabel.Name = "pageModelPropertyNameLabel";
			pageModelPropertyNameLabel.Size = new Size (400, 37);
			pageModelPropertyNameLabel.TabIndex = 1;
			pageModelPropertyNameLabel.Text = "Enter Page Model Property Name here";
			// 
			// pageModelPropertyNameTextBox
			// 
			pageModelPropertyNameTextBox.BackColor = Color.FromArgb (  64,   64,   64);
			pageModelPropertyNameTextBox.BorderStyle = BorderStyle.FixedSingle;
			pageModelPropertyNameTextBox.Font = new Font ("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point,  0);
			pageModelPropertyNameTextBox.ForeColor = Color.White;
			pageModelPropertyNameTextBox.Location = new Point (970, 60);
			pageModelPropertyNameTextBox.Name = "pageModelPropertyNameTextBox";
			pageModelPropertyNameTextBox.Size = new Size (401, 36);
			pageModelPropertyNameTextBox.TabIndex = 0;
			pageModelPropertyNameTextBox.TextChanged += pageModelPropertyNameTextBox_TextChanged;
			// 
			// nameOkPictureBox
			// 
			nameOkPictureBox.Image = Resources.StudioResources.Ok;
			nameOkPictureBox.Location = new Point (1380, 60);
			nameOkPictureBox.Name = "nameOkPictureBox";
			nameOkPictureBox.Size = new Size (36, 37);
			nameOkPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			nameOkPictureBox.TabIndex = 6;
			nameOkPictureBox.TabStop = false;
			// 
			// nameOkLabel
			// 
			nameOkLabel.BackColor = Color.LightCyan;
			nameOkLabel.Font = new Font ("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point,  0);
			nameOkLabel.Location = new Point (1374, 20);
			nameOkLabel.Name = "nameOkLabel";
			nameOkLabel.Size = new Size (100, 37);
			nameOkLabel.TabIndex = 1;
			nameOkLabel.Text = "Name OK?";
			// 
			// whyNameNotOkTooltip
			// 
			whyNameNotOkTooltip.ToolTipIcon = ToolTipIcon.Warning;
			whyNameNotOkTooltip.ToolTipTitle = "Property Name must end with `Span`";
			// 
			// doNotInitializeInConstructorCheckBox
			// 
			doNotInitializeInConstructorCheckBox.AutoSize = true;
			doNotInitializeInConstructorCheckBox.BackColor = Color.LightCyan;
			doNotInitializeInConstructorCheckBox.Location = new Point (970, 110);
			doNotInitializeInConstructorCheckBox.Margin = new Padding (4, 5, 4, 5);
			doNotInitializeInConstructorCheckBox.Name = "doNotInitializeInConstructorCheckBox";
			doNotInitializeInConstructorCheckBox.Size = new Size (278, 29);
			doNotInitializeInConstructorCheckBox.TabIndex = 7;
			doNotInitializeInConstructorCheckBox.Text = "&Do not initialize in Constructor";
			whyNameNotOkTooltip.SetToolTip (doNotInitializeInConstructorCheckBox, "Check this if your UI element comes as a result of a User activity (e.g. clicking accordion, mandatory field message, etc.)");
			doNotInitializeInConstructorCheckBox.UseVisualStyleBackColor = false;
			doNotInitializeInConstructorCheckBox.CheckedChanged += doNotInitializeInConstructorCheckBox_CheckedChanged;
			// 
			// UIControlHtmlTagMapperControl
			// 
			AutoScaleDimensions = new SizeF (10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add (doNotInitializeInConstructorCheckBox);
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
			Size = new Size (1810, 153);
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
		private CheckBox doNotInitializeInConstructorCheckBox;
	}
}
