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
			htmlTagInfoFlowLayoutPanel = new FlowLayoutPanel ();
			buildPageCodeButton = new Button ();
			quitButton = new Button ();
			savePageModelCodeFileDialog = new SaveFileDialog ();
			pageDetailsGroupBox.SuspendLayout ();
			groupBox1.SuspendLayout ();
			SuspendLayout ();
			// 
			// customSpecificationsLabel
			// 
			customSpecificationsLabel.Font = new Font ("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
			customSpecificationsLabel.Location = new Point (14, 10);
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
			pageDetailsGroupBox.Location = new Point (18, 69);
			pageDetailsGroupBox.Name = "pageDetailsGroupBox";
			pageDetailsGroupBox.Size = new Size (1924, 150);
			pageDetailsGroupBox.TabIndex = 1;
			pageDetailsGroupBox.TabStop = false;
			pageDetailsGroupBox.Text = "Page Model Details";
			// 
			// namespaceTextBox
			// 
			namespaceTextBox.BackColor = Color.FromArgb (64, 64, 64);
			namespaceTextBox.Font = new Font ("Consolas", 12F, FontStyle.Bold);
			namespaceTextBox.ForeColor = Color.White;
			namespaceTextBox.Location = new Point (363, 92);
			namespaceTextBox.Name = "namespaceTextBox";
			namespaceTextBox.Size = new Size (722, 36);
			namespaceTextBox.TabIndex = 1;
			namespaceTextBox.Text = "MyApp.MyFunctionality.MyPurpose";
			// 
			// pageModelNameTextBox
			// 
			pageModelNameTextBox.BackColor = Color.FromArgb (64, 64, 64);
			pageModelNameTextBox.Font = new Font ("Consolas", 12F, FontStyle.Bold);
			pageModelNameTextBox.ForeColor = Color.White;
			pageModelNameTextBox.Location = new Point (363, 42);
			pageModelNameTextBox.Name = "pageModelNameTextBox";
			pageModelNameTextBox.Size = new Size (722, 36);
			pageModelNameTextBox.TabIndex = 0;
			pageModelNameTextBox.Text = "MyApplicationPage";
			// 
			// namespaceLabel
			// 
			namespaceLabel.Location = new Point (26, 90);
			namespaceLabel.Name = "namespaceLabel";
			namespaceLabel.Size = new Size (331, 38);
			namespaceLabel.TabIndex = 3;
			namespaceLabel.Text = "Namespace:";
			namespaceLabel.TextAlign = ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			label1.Location = new Point (1091, 90);
			label1.Name = "label1";
			label1.Size = new Size (331, 38);
			label1.TabIndex = 3;
			label1.Text = "e.g.: NetBankingApp.WebUITests.Login";
			label1.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// pageModelNameExamplesLabel
			// 
			pageModelNameExamplesLabel.Location = new Point (1091, 40);
			pageModelNameExamplesLabel.Name = "pageModelNameExamplesLabel";
			pageModelNameExamplesLabel.Size = new Size (331, 38);
			pageModelNameExamplesLabel.TabIndex = 2;
			pageModelNameExamplesLabel.Text = "e.g.: NetBankingAppLoginPage";
			pageModelNameExamplesLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// pageModelNameLabel
			// 
			pageModelNameLabel.Location = new Point (26, 40);
			pageModelNameLabel.Name = "pageModelNameLabel";
			pageModelNameLabel.Size = new Size (331, 38);
			pageModelNameLabel.TabIndex = 2;
			pageModelNameLabel.Text = "Page Model Name:";
			pageModelNameLabel.TextAlign = ContentAlignment.MiddleRight;
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add (htmlTagInfoFlowLayoutPanel);
			groupBox1.Location = new Point (18, 238);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size (1924, 1049);
			groupBox1.TabIndex = 4;
			groupBox1.TabStop = false;
			groupBox1.Text = "Web Page UI Controls && HTML Tags Mapping";
			// 
			// flowLayoutPanel1
			// 
			htmlTagInfoFlowLayoutPanel.AutoScroll = true;
			htmlTagInfoFlowLayoutPanel.Location = new Point (12, 30);
			htmlTagInfoFlowLayoutPanel.Name = "flowLayoutPanel1";
			htmlTagInfoFlowLayoutPanel.Size = new Size (1900, 1005);
			htmlTagInfoFlowLayoutPanel.TabIndex = 2;
			// 
			// buildPageCodeButton
			// 
			buildPageCodeButton.Font = new Font ("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			buildPageCodeButton.ForeColor = Color.Green;
			buildPageCodeButton.Location = new Point (1606, 1298);
			buildPageCodeButton.Name = "buildPageCodeButton";
			buildPageCodeButton.Size = new Size (339, 57);
			buildPageCodeButton.TabIndex = 6;
			buildPageCodeButton.Text = "Build Web Page C# Code";
			buildPageCodeButton.UseVisualStyleBackColor = true;
			buildPageCodeButton.Click += buildPageCodeButton_Click;
			// 
			// quitButton
			// 
			quitButton.Font = new Font ("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
			quitButton.ForeColor = Color.Black;
			quitButton.Location = new Point (1455, 1298);
			quitButton.Name = "quitButton";
			quitButton.Size = new Size (145, 57);
			quitButton.TabIndex = 7;
			quitButton.Text = "&Cancel";
			quitButton.UseVisualStyleBackColor = true;
			quitButton.Click += quitButton_Click;
			// 
			// savePageModelCodeFileDialog
			// 
			savePageModelCodeFileDialog.AddToRecent = false;
			savePageModelCodeFileDialog.CheckFileExists = true;
			savePageModelCodeFileDialog.DefaultExt = "cs";
			savePageModelCodeFileDialog.Filter = "C# code files | *.cs";
			savePageModelCodeFileDialog.FilterIndex = 0;
			savePageModelCodeFileDialog.Title = "Please specify where Page Model C# file is to be saved";
			// 
			// WebPageModelDetailsScreen
			// 
			AcceptButton = buildPageCodeButton;
			AutoScaleDimensions = new SizeF (10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = quitButton;
			ClientSize = new Size (1957, 1367);
			Controls.Add (quitButton);
			Controls.Add (buildPageCodeButton);
			Controls.Add (groupBox1);
			Controls.Add (pageDetailsGroupBox);
			Controls.Add (customSpecificationsLabel);
			Icon = (Icon) resources.GetObject ("$this.Icon");
			MaximizeBox = false;
			Name = "WebPageModelDetailsScreen";
			StartPosition = FormStartPosition.CenterParent;
			Text = "Web Page Model Details";
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
		private FlowLayoutPanel htmlTagInfoFlowLayoutPanel;
		private Button buildPageCodeButton;
		private Button quitButton;
		private SaveFileDialog savePageModelCodeFileDialog;
	}
}