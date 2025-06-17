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
			nOfTotalLabel.Location = new Point (7, 6);
			nOfTotalLabel.Margin = new Padding (2, 0, 2, 0);
			nOfTotalLabel.Name = "nOfTotalLabel";
			nOfTotalLabel.Size = new Size (84, 60);
			nOfTotalLabel.TabIndex = 0;
			nOfTotalLabel.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// htmlTagNameLabel
			// 
			htmlTagNameLabel.BackColor = Color.LightCyan;
			htmlTagNameLabel.Font = new Font ("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point,  0);
			htmlTagNameLabel.Location = new Point (102, 12);
			htmlTagNameLabel.Margin = new Padding (2, 0, 2, 0);
			htmlTagNameLabel.Name = "htmlTagNameLabel";
			htmlTagNameLabel.Size = new Size (280, 22);
			htmlTagNameLabel.TabIndex = 1;
			htmlTagNameLabel.Text = "HTML Tag Name";
			// 
			// htmlTagNameValueLabel
			// 
			htmlTagNameValueLabel.BorderStyle = BorderStyle.FixedSingle;
			htmlTagNameValueLabel.Font = new Font ("Consolas", 10F, FontStyle.Bold, GraphicsUnit.Point,  0);
			htmlTagNameValueLabel.Location = new Point (105, 36);
			htmlTagNameValueLabel.Margin = new Padding (2, 0, 2, 0);
			htmlTagNameValueLabel.Name = "htmlTagNameValueLabel";
			htmlTagNameValueLabel.Size = new Size (281, 22);
			htmlTagNameValueLabel.TabIndex = 2;
			htmlTagNameValueLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// previewPictureBox
			// 
			previewPictureBox.BorderStyle = BorderStyle.FixedSingle;
			previewPictureBox.Location = new Point (1050, 6);
			previewPictureBox.Margin = new Padding (2, 2, 2, 2);
			previewPictureBox.Name = "previewPictureBox";
			previewPictureBox.Size = new Size (211, 88);
			previewPictureBox.TabIndex = 3;
			previewPictureBox.TabStop = false;
			// 
			// mappedControlNameValueLabel
			// 
			mappedControlNameValueLabel.BackColor = Color.Black;
			mappedControlNameValueLabel.BorderStyle = BorderStyle.FixedSingle;
			mappedControlNameValueLabel.Font = new Font ("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point,  0);
			mappedControlNameValueLabel.ForeColor = Color.Lime;
			mappedControlNameValueLabel.Location = new Point (392, 36);
			mappedControlNameValueLabel.Margin = new Padding (2, 0, 2, 0);
			mappedControlNameValueLabel.Name = "mappedControlNameValueLabel";
			mappedControlNameValueLabel.Size = new Size (281, 22);
			mappedControlNameValueLabel.TabIndex = 2;
			mappedControlNameValueLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// mappedControlNameLabel
			// 
			mappedControlNameLabel.BackColor = Color.LightCyan;
			mappedControlNameLabel.Font = new Font ("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point,  0);
			mappedControlNameLabel.Location = new Point (392, 12);
			mappedControlNameLabel.Margin = new Padding (2, 0, 2, 0);
			mappedControlNameLabel.Name = "mappedControlNameLabel";
			mappedControlNameLabel.Size = new Size (280, 22);
			mappedControlNameLabel.TabIndex = 1;
			mappedControlNameLabel.Text = "Mapped OOSF Control";
			// 
			// backgroundLabel
			// 
			backgroundLabel.BackColor = Color.LightCyan;
			backgroundLabel.BorderStyle = BorderStyle.Fixed3D;
			backgroundLabel.Location = new Point (98, 6);
			backgroundLabel.Margin = new Padding (2, 0, 2, 0);
			backgroundLabel.Name = "backgroundLabel";
			backgroundLabel.Size = new Size (945, 86);
			backgroundLabel.TabIndex = 4;
			// 
			// pageModelPropertyNameLabel
			// 
			pageModelPropertyNameLabel.BackColor = Color.LightCyan;
			pageModelPropertyNameLabel.Font = new Font ("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point,  0);
			pageModelPropertyNameLabel.Location = new Point (679, 12);
			pageModelPropertyNameLabel.Margin = new Padding (2, 0, 2, 0);
			pageModelPropertyNameLabel.Name = "pageModelPropertyNameLabel";
			pageModelPropertyNameLabel.Size = new Size (280, 22);
			pageModelPropertyNameLabel.TabIndex = 1;
			pageModelPropertyNameLabel.Text = "Enter Page Model Property Name here";
			// 
			// pageModelPropertyNameTextBox
			// 
			pageModelPropertyNameTextBox.BackColor = Color.FromArgb (  64,   64,   64);
			pageModelPropertyNameTextBox.BorderStyle = BorderStyle.FixedSingle;
			pageModelPropertyNameTextBox.Font = new Font ("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point,  0);
			pageModelPropertyNameTextBox.ForeColor = Color.White;
			pageModelPropertyNameTextBox.Location = new Point (679, 36);
			pageModelPropertyNameTextBox.Margin = new Padding (2, 2, 2, 2);
			pageModelPropertyNameTextBox.Name = "pageModelPropertyNameTextBox";
			pageModelPropertyNameTextBox.Size = new Size (281, 26);
			pageModelPropertyNameTextBox.TabIndex = 0;
			pageModelPropertyNameTextBox.TextChanged += pageModelPropertyNameTextBox_TextChanged;
			// 
			// nameOkPictureBox
			// 
			nameOkPictureBox.Image = Resources.StudioResources.Ok;
			nameOkPictureBox.Location = new Point (966, 36);
			nameOkPictureBox.Margin = new Padding (2, 2, 2, 2);
			nameOkPictureBox.Name = "nameOkPictureBox";
			nameOkPictureBox.Size = new Size (25, 22);
			nameOkPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			nameOkPictureBox.TabIndex = 6;
			nameOkPictureBox.TabStop = false;
			// 
			// nameOkLabel
			// 
			nameOkLabel.BackColor = Color.LightCyan;
			nameOkLabel.Font = new Font ("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point,  0);
			nameOkLabel.Location = new Point (962, 12);
			nameOkLabel.Margin = new Padding (2, 0, 2, 0);
			nameOkLabel.Name = "nameOkLabel";
			nameOkLabel.Size = new Size (70, 22);
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
			doNotInitializeInConstructorCheckBox.Location = new Point (679, 72);
			doNotInitializeInConstructorCheckBox.Name = "doNotInitializeInConstructorCheckBox";
			doNotInitializeInConstructorCheckBox.Size = new Size (187, 19);
			doNotInitializeInConstructorCheckBox.TabIndex = 7;
			doNotInitializeInConstructorCheckBox.Text = "&Do not initialize in Constructor";
			whyNameNotOkTooltip.SetToolTip (doNotInitializeInConstructorCheckBox, "Check this if your UI element comes as a result of a User activity (e.g. clicking accordion, mandatory field message, etc.)");
			doNotInitializeInConstructorCheckBox.UseVisualStyleBackColor = false;
			doNotInitializeInConstructorCheckBox.CheckedChanged += doNotInitializeInConstructorCheckBox_CheckedChanged;
			// 
			// UIControlHtmlTagMapperControl
			// 
			AutoScaleDimensions = new SizeF (7F, 15F);
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
			Margin = new Padding (2, 2, 2, 2);
			Name = "UIControlHtmlTagMapperControl";
			Size = new Size (1267, 98);
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
