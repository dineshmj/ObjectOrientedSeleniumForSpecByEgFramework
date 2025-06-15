using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;

using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.WebUIPageStudio.Entities;
using OOSF = OOSelenium.Framework.WebUIControls;
using OOSelenium.Framework.Extensions;
using OOSelenium.Framework.WebUIControls;

namespace OOSelenium.WebUIPageStudio
{
	public partial class WebPageModelDetailsScreen
		: Form
	{
		private static WebPageModelDetailsScreen? instance;
		private static object syncLocker = new ();

		public static WebPageModelDetailsScreen DefinedInstance
		{
			get
			{
				lock (syncLocker)
				{
					if (instance == null || instance.IsDisposed)
					{
						instance = new WebPageModelDetailsScreen ();
					}
				}
				return instance;
			}
		}

		private WebPageModelDetailsScreen ()
		{
			InitializeComponent ();
		}

		public void LoadSelectedElements (IEnumerable<HtmlTagInfo> htmlTagInfos)
		{
			if (htmlTagInfos == null || !htmlTagInfos.Any ())
			{
				throw new ArgumentNullException (nameof (htmlTagInfos), "The collection of HTML Tag Info instances cannot be null or empty.");
			}

			this.htmlTagInfoFlowLayoutPanel.Controls.Clear ();

			int index = 1;
			int total = htmlTagInfos.Count ();

			foreach (var htmlTagInfo in htmlTagInfos)
			{
				var control = new UIControlHtmlTagMapperControl ();
				control.MapHtmlTagInfo (htmlTagInfo, index, total);

				this.htmlTagInfoFlowLayoutPanel.Controls.Add (control);
				index++;
			}
		}

		private void buildPageCodeButton_Click (object sender, EventArgs e)
		{
			foreach (var oneHtmlTagCustomControl in this.htmlTagInfoFlowLayoutPanel.Controls.OfType<UIControlHtmlTagMapperControl> ())
			{
				if (oneHtmlTagCustomControl.IsNameValid == false)
				{
					MessageBox.Show ("Please provide a valid property name for all HTML tags.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}

			if (MessageBox.Show ("Are you sure you want to build the page model code?", "Confirm Build", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				this.savePageModelCodeFileDialog.FileName = this.pageModelNameTextBox.Text.Trim ();

				if (this.savePageModelCodeFileDialog.ShowDialog () == DialogResult.OK)
				{
					var pageModelFilePath = this.savePageModelCodeFileDialog.FileName;

					if (this.BuildPageModelClassCode (pageModelFilePath))
					{
						MessageBox.Show ("Page model code built successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			}
		}

		private bool BuildPageModelClassCode (string pageModelFilePath)
		{
			try
			{
				// Create a CodeDOM structure for the page model class.
				CodeCompileUnit compileUnit = new ();

				// Set the namespace for the page model class.
				var globalNamespace = new CodeNamespace (string.Empty);

				// Add necessary imports to the namespace.
				globalNamespace.Imports.Add (new CodeNamespaceImport ("OpenQA.Selenium"));
				globalNamespace.Imports.Add (new CodeNamespaceImport ("OOSelenium.Framework.Abstractions"));
				globalNamespace.Imports.Add (new CodeNamespaceImport ("OOSelenium.Framework.WebUIControls"));

				compileUnit.Namespaces.Add (globalNamespace);

				var pageModelNamespace = new CodeNamespace (this.namespaceTextBox.Text);
				compileUnit.Namespaces.Add (pageModelNamespace);

				// Create the page model class.
				var pageModelClass = new CodeTypeDeclaration (this.pageModelNameTextBox.Text);
				pageModelClass.IsClass = true;
				pageModelClass.TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed;
				pageModelClass.BaseTypes.Add (new CodeTypeReference (typeof (WebUiPageBase).Name));

				// Add the page model class to the namespace.
				pageModelNamespace.Types.Add (pageModelClass);

				foreach (var oneHtmlTagCustomControl in this.htmlTagInfoFlowLayoutPanel.Controls.OfType<UIControlHtmlTagMapperControl> ())
				{
					// Create a field for each HTML tag info.
					var property = new CodeSnippetTypeMember ($"\t\tpublic {oneHtmlTagCustomControl.MappedOOSFWebUIControlName} {oneHtmlTagCustomControl.UserSuggestedPropertyName} {{ get; init; }}\r\n");
					pageModelClass.Members.Add (property);
				}

				// Add a constructor to initialize the fields.
				var constructor = new CodeConstructor
				{
					Attributes = MemberAttributes.Public
				};

				constructor.Parameters.Add (new CodeParameterDeclarationExpression (typeof (IWebDriver).Name, "webDriver"));
				constructor.Parameters.Add (new CodeParameterDeclarationExpression (typeof (string).Name, "baseUrl"));

				constructor.BaseConstructorArgs.Add (new CodeVariableReferenceExpression ("webDriver"));
				constructor.BaseConstructorArgs.Add (new CodeVariableReferenceExpression ("baseUrl"));

				pageModelClass.Members.Add (constructor);

				// Loop through each Tag Info control instance..
				var index = 0;
				foreach (var oneHtmlTagCustomControl in this.htmlTagInfoFlowLayoutPanel.Controls.OfType<UIControlHtmlTagMapperControl> ())
				{
					var fieldType = oneHtmlTagCustomControl.MappedOOSFWebUIControlName;

					if (fieldType == nameof (OOSF.RadioButtons))
					{
						constructor
							.Statements
							.Add (
								new CodeAssignStatement (
									new CodeVariableReferenceExpression (oneHtmlTagCustomControl.UserSuggestedPropertyName),
									new CodeMethodInvokeExpression (
										new CodeThisReferenceExpression (),
										// Method to find radio button group by name.
										nameof (WebUiPageBase.CodeDomHelper.FindRadioButtonGroupByName),
										// Name of radio button group.
										new CodePrimitiveExpression (oneHtmlTagCustomControl.Text)
									)
								)
							);
					}
					else if (fieldType == nameof (OOSF.DropDownList))
					{
						constructor
							.Statements
							.Add (
								new CodeAssignStatement (
									new CodeVariableReferenceExpression (oneHtmlTagCustomControl.UserSuggestedPropertyName),
									new CodeMethodInvokeExpression (
										new CodeThisReferenceExpression (),
										// Method to find dropdown list by name.
										nameof (WebUiPageBase.CodeDomHelper.FindDropDownList),
										// Name of the dropdown list.
										new CodePrimitiveExpression (oneHtmlTagCustomControl.HtmlTagInfo.Name)
									)
								)
							);
					}
					else if (fieldType == nameof (OOSF.MultiSelectListBox))
					{
						constructor
							.Statements
							.Add (
								new CodeAssignStatement (
									new CodeVariableReferenceExpression (oneHtmlTagCustomControl.UserSuggestedPropertyName),
									new CodeMethodInvokeExpression (
										new CodeThisReferenceExpression (),
										// Method to find multi-select list box by name.
										nameof (WebUiPageBase.CodeDomHelper.FindMultiSelectListBox),
										// Name of the multi-select list box.
										new CodePrimitiveExpression (oneHtmlTagCustomControl.HtmlTagInfo.Description.Replace ($"{ oneHtmlTagCustomControl.MappedOOSFWebUIControlName } ", string.Empty).Replace ("'", string.Empty))
									)
								)
							);
					}
					else
					{
						if (oneHtmlTagCustomControl.HtmlTagInfo.Id.IsNotNullEmptyOrWhitespace ())
						{
							// "id" of the HTML tag is present; use it to find the control.
							constructor.Statements.Add (
								new CodeSnippetStatement (
									$"            {oneHtmlTagCustomControl.UserSuggestedPropertyName} = base.FindById<{oneHtmlTagCustomControl.MappedOOSFWebUIControlName}>(" +
									$"\"{oneHtmlTagCustomControl.HtmlTagInfo.Id}\", " +
									$"(id, webElement, webDriver) => new {oneHtmlTagCustomControl.MappedOOSFWebUIControlName} (webElement, id, LocateByWhat.Id, webDriver)" +
									");"
								)
							);
						}
						else
						{
							// "id" of the HTML tag is not present; use XPath to find the control.
							constructor.Statements.Add (
								new CodeSnippetStatement (
									$"            {oneHtmlTagCustomControl.UserSuggestedPropertyName} = base.FindByXPath<{oneHtmlTagCustomControl.MappedOOSFWebUIControlName}>(" +
									$"\"{oneHtmlTagCustomControl.HtmlTagInfo.XPath.Replace ("\"", "\\\"")}\", " +
									$"(xPath, webElement, webDriver) => new {oneHtmlTagCustomControl.MappedOOSFWebUIControlName} (webElement, xPath, LocateByWhat.XPath, webDriver)" +
									");"
								)
							);
						}
					}

					index++;
					if (index < this.htmlTagInfoFlowLayoutPanel.Controls.Count)
					{
						constructor.Statements.Add (new CodeSnippetStatement (string.Empty)); // Add a blank line between statements for readability.
					}
				}

				using (CodeDomProvider provider = CodeDomProvider.CreateProvider ("CSharp"))
				{
					// CodeGeneratorOptions options
					var options = new CodeGeneratorOptions
					{
						BracingStyle = "C", // C-style braces (opening brace on same line as declaration)
						BlankLinesBetweenMembers = true, // Add blank lines between members
						IndentString = "\t" // Use tab spaces for indentation
					};

					// Create a TextWriter to write the code to a file
					using (var streamWriter = new StreamWriter (pageModelFilePath, false)) // false means overwrite if file exists
					{
						provider.GenerateCodeFromCompileUnit (compileUnit, streamWriter, options);
					}

					return true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show ($"An error occurred while building the page model code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}

		private void quitButton_Click (object sender, EventArgs e)
		{
			if (MessageBox.Show ("Are you sure you want to quit?", "Confirm Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				this.Close ();
			}
		}
	}
}