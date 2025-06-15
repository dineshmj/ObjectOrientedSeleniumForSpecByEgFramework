namespace OOSelenium.WebUIPageStudio
{
	partial class WebPageModelDetailsScreen
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager (typeof (WebPageModelDetailsScreen));
			customSpecificationsLabel = new Label ();
			pageDetailsGroupBox = new GroupBox ();
			namespaceTextBox = new TextBox ();
			pageModelNameTextBox = new TextBox ();
			namespaceLabel = new Label ();
			label1 = new Label ();
			pageModelNameExamplesLabel = new Label ();
			pageModelNameLabel = new Label ();
			groupBox1 = new GroupBox ();
			flowLayoutPanel1 = new FlowLayoutPanel ();
			pageDetailsGroupBox.SuspendLayout ();
			groupBox1.SuspendLayout ();
			SuspendLayout ();
			// 
			// customSpecificationsLabel
			// 
			customSpecificationsLabel.Font = new Font ("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point,  0);
			customSpecificationsLabel.Location = new Point (29, 17);
			customSpecificationsLabel.Name = "customSpecificationsLabel";
			customSpecificationsLabel.Size = new Size (840, 50);
			customSpecificationsLabel.TabIndex = 0;
			customSpecificationsLabel.Text = "Web Page Model C# Class - Custom Specifications";
			// 
			// pageDetailsGroupBox
			// 
			pageDetailsGroupBox.Controls.Add (namespaceTextBox);
			pageDetailsGroupBox.Controls.Add (pageModelNameTextBox);
			pageDetailsGroupBox.Controls.Add (namespaceLabel);
			pageDetailsGroupBox.Controls.Add (label1);
			pageDetailsGroupBox.Controls.Add (pageModelNameExamplesLabel);
			pageDetailsGroupBox.Controls.Add (pageModelNameLabel);
			pageDetailsGroupBox.Location = new Point (37, 76);
			pageDetailsGroupBox.Name = "pageDetailsGroupBox";
			pageDetailsGroupBox.Size = new Size (1924, 150);
			pageDetailsGroupBox.TabIndex = 1;
			pageDetailsGroupBox.TabStop = false;
			pageDetailsGroupBox.Text = "Page Model Details";
			// 
			// namespaceTextBox
			// 
			namespaceTextBox.BackColor = Color.FromArgb (  64,   64,   64);
			namespaceTextBox.Font = new Font ("Consolas", 12F, FontStyle.Bold);
			namespaceTextBox.ForeColor = Color.White;
			namespaceTextBox.Location = new Point (382, 99);
			namespaceTextBox.Name = "namespaceTextBox";
			namespaceTextBox.Size = new Size (562, 36);
			namespaceTextBox.TabIndex = 1;
			namespaceTextBox.Text = "MyApp.MyFunctionality.MyPurpose";
			// 
			// pageModelNameTextBox
			// 
			pageModelNameTextBox.BackColor = Color.FromArgb (  64,   64,   64);
			pageModelNameTextBox.Font = new Font ("Consolas", 12F, FontStyle.Bold);
			pageModelNameTextBox.ForeColor = Color.White;
			pageModelNameTextBox.Location = new Point (382, 49);
			pageModelNameTextBox.Name = "pageModelNameTextBox";
			pageModelNameTextBox.Size = new Size (562, 36);
			pageModelNameTextBox.TabIndex = 0;
			pageModelNameTextBox.Text = "LandingPage";
			// 
			// namespaceLabel
			// 
			namespaceLabel.Location = new Point (45, 97);
			namespaceLabel.Name = "namespaceLabel";
			namespaceLabel.Size = new Size (331, 38);
			namespaceLabel.TabIndex = 3;
			namespaceLabel.Text = "Namespace:";
			namespaceLabel.TextAlign = ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			label1.Location = new Point (950, 97);
			label1.Name = "label1";
			label1.Size = new Size (331, 38);
			label1.TabIndex = 3;
			label1.Text = "e.g.: NetBankingApp.WebUITests.Login";
			label1.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// pageModelNameExamplesLabel
			// 
			pageModelNameExamplesLabel.Location = new Point (950, 47);
			pageModelNameExamplesLabel.Name = "pageModelNameExamplesLabel";
			pageModelNameExamplesLabel.Size = new Size (331, 38);
			pageModelNameExamplesLabel.TabIndex = 2;
			pageModelNameExamplesLabel.Text = "e.g.: NetBankingAppLoginPage";
			pageModelNameExamplesLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// pageModelNameLabel
			// 
			pageModelNameLabel.Location = new Point (45, 47);
			pageModelNameLabel.Name = "pageModelNameLabel";
			pageModelNameLabel.Size = new Size (331, 38);
			pageModelNameLabel.TabIndex = 2;
			pageModelNameLabel.Text = "Page Model Name:";
			pageModelNameLabel.TextAlign = ContentAlignment.MiddleRight;
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add (flowLayoutPanel1);
			groupBox1.Location = new Point (37, 245);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size (1924, 1057);
			groupBox1.TabIndex = 4;
			groupBox1.TabStop = false;
			groupBox1.Text = "Web Page UI Controls && HTML Tags Mapping";
			// 
			// flowLayoutPanel1
			// 
			flowLayoutPanel1.AutoScroll = true;
			flowLayoutPanel1.Location = new Point (12, 37);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new Size (1889, 1005);
			flowLayoutPanel1.TabIndex = 2;
			// 
			// WebPageModelDetailsScreen
			// 
			AutoScaleDimensions = new SizeF (10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size (2000, 1326);
			Controls.Add (groupBox1);
			Controls.Add (pageDetailsGroupBox);
			Controls.Add (customSpecificationsLabel);
			Icon = (Icon) resources.GetObject ("$this.Icon");
			MaximizeBox = false;
			Name = "WebPageModelDetailsScreen";
			StartPosition = FormStartPosition.CenterParent;
			Text = "WebPageModelDetailsScreen";
			pageDetailsGroupBox.ResumeLayout (false);
			pageDetailsGroupBox.PerformLayout ();
			groupBox1.ResumeLayout (false);
			ResumeLayout (false);
		}

		#endregion

		private Label customSpecificationsLabel;
		private GroupBox pageDetailsGroupBox;
		private Label pageModelNameLabel;
		private TextBox pageModelNameTextBox;
		private TextBox namespaceTextBox;
		private Label namespaceLabel;
		private Label pageModelNameExamplesLabel;
		private Label label1;
		private GroupBox groupBox1;
		private FlowLayoutPanel flowLayoutPanel1;
	}
}