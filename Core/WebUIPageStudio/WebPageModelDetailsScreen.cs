using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;

using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.WebUIPageStudio.Entities;
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

		public void SetSuggestedPageName (string suggestedPageName)
		{
			this.pageModelNameTextBox.Text = suggestedPageName;
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

			this.LoadLastUsedNamesToTextFields ();
		}

		private void buildPageCodeButton_Click (object sender, EventArgs e)
		{
			var properties = this.htmlTagInfoFlowLayoutPanel.Controls.OfType<UIControlHtmlTagMapperControl> ();
			var propertyNames = properties.Select (c => c.UserSuggestedPropertyName.Trim ()).ToList ();

			// Check if property name repeats in the array.
			if (propertyNames.Distinct ().Count () != propertyNames.Count)
			{
				MessageBox.Show ("Property names must be unique. Please ensure all property names are distinct.", "Property names must be distinct", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			foreach (var oneProperty in properties)
			{
				if (oneProperty.IsNameValid == false)
				{
					MessageBox.Show ("Please provide a valid property name for all HTML tags.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}

			if (MessageBox.Show ("Are you sure you want to build the page model code?", "Confirm Build", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				var lines = new List<string> ();

				var iniFilePath = Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "LastUsed.ini");
				lines.Add ($"Namespace={this.namespaceTextBox.Text.Trim ()}");
				lines.Add ($"PageModelName={this.pageModelNameTextBox.Text.Trim ()}");

				var propertyLines
					= properties
						.Select (c => $"PropertyInfo={c.MappedOOSFWebUIControlName}:{c.UserSuggestedPropertyName}");

				lines.AddRange (propertyLines);
				File.WriteAllLines (iniFilePath, lines);

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
				globalNamespace.Imports.Add (new CodeNamespaceImport ("OOSelenium.Framework.Entities"));
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

				var properties = this.htmlTagInfoFlowLayoutPanel.Controls.OfType<UIControlHtmlTagMapperControl> ();

				// Loop through each Tag Info control instance..
				foreach (var oneHtmlTagCustomControl in properties)
				{
					var fieldType = oneHtmlTagCustomControl.MappedOOSFWebUIControlName;
					var propertyName = oneHtmlTagCustomControl.UserSuggestedPropertyName;
					var privateFieldName = $"_{char.ToLower (propertyName [0])}{propertyName.Substring (1)}";

					var privateField = new CodeMemberField
					{
						Name = privateFieldName,
						Type = new CodeTypeReference (fieldType),
						Attributes = MemberAttributes.Private
					};

					pageModelClass.Members.Add (privateField);

				}
				foreach (var oneHtmlTagCustomControl in properties)
				{
					if (oneHtmlTagCustomControl.DoNotInitializeInConstructor)
					{
						var propertyWithStatements
							= this.DefinePropertyOutsideConstructor (oneHtmlTagCustomControl);

						pageModelClass.Members.Add (propertyWithStatements);
					}
					else
					{
						// Create a field for each HTML tag info that will be initialized in the constructor.
						var propertyWithGetAndInit
							= new CodeSnippetTypeMember ($"\t\tpublic {oneHtmlTagCustomControl.MappedOOSFWebUIControlName} {oneHtmlTagCustomControl.UserSuggestedPropertyName} {{ get; init; }}\r\n");

						pageModelClass.Members.Add (propertyWithGetAndInit);
					}
				}

				// Add a constructor to initialize the fields.
				var constructor = new CodeConstructor
				{
					Attributes = MemberAttributes.Public
				};

				constructor.Parameters.Add (new CodeParameterDeclarationExpression (typeof (IWebDriver).Name, "webDriver"));
				constructor.Parameters.Add (new CodeParameterDeclarationExpression (new CodeTypeReference (typeof (string)), "baseUrl"));
				constructor.Parameters.Add (new CodeParameterDeclarationExpression (new CodeTypeReference (typeof (bool)), "navigationRequired"));
				constructor.Parameters.Add (new CodeParameterDeclarationExpression (new CodeTypeReference (typeof (bool)), "maximizeWindow"));

				constructor.BaseConstructorArgs.Add (new CodeVariableReferenceExpression ("webDriver"));
				constructor.BaseConstructorArgs.Add (new CodeVariableReferenceExpression ("baseUrl"));
				constructor.BaseConstructorArgs.Add (new CodeVariableReferenceExpression ("navigationRequired"));
				constructor.BaseConstructorArgs.Add (new CodeVariableReferenceExpression ("maximizeWindow"));

				pageModelClass.Members.Add (constructor);

				// Loop through each Tag Info control instance to add initialization code in the constructor.
				var index = 0;
				foreach (var oneHtmlTagCustomControl in properties)
				{
					if (oneHtmlTagCustomControl.DoNotInitializeInConstructor)
					{
						// Skip controls that are not initialized in the constructor.
						continue;
					}

					var fieldType = oneHtmlTagCustomControl.MappedOOSFWebUIControlName;

					if (fieldType == nameof (RadioButtons))
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
					else if (fieldType == nameof (DropDownList))
					{
						constructor
							.Statements
							.Add (
								new CodeAssignStatement (
									new CodeVariableReferenceExpression (oneHtmlTagCustomControl.UserSuggestedPropertyName),
									new CodeMethodInvokeExpression (
										new CodeThisReferenceExpression (),
										// Method to find dropdown list by name.
										nameof (WebUiPageBase.CodeDomHelper.FindDropDownListByXPath),
										// Name of the dropdown list.
										new CodePrimitiveExpression (oneHtmlTagCustomControl.HtmlTagInfo.Name)
									)
								)
							);
					}
					else if (fieldType == nameof (MultiSelectListBox))
					{
						constructor
							.Statements
							.Add (
								new CodeAssignStatement (
									new CodeVariableReferenceExpression (oneHtmlTagCustomControl.UserSuggestedPropertyName),
									new CodeMethodInvokeExpression (
										new CodeThisReferenceExpression (),
										// Method to find multi-select list box by name.
										nameof (WebUiPageBase.CodeDomHelper.FindMultiSelectListBoxByXPath),
										// Name of the multi-select list box.
										new CodePrimitiveExpression (oneHtmlTagCustomControl.HtmlTagInfo.Description.Replace ($"{ oneHtmlTagCustomControl.MappedOOSFWebUIControlName } ", string.Empty).Replace ("'", string.Empty))
									)
								)
							);
					}
					else
					{
						// "id" of the HTML tag is not reliable (especially with Pega and Salesforce web pages
						// So, use XPath to find the control.
						constructor.Statements.Add (
							new CodeSnippetStatement (
								$"            {oneHtmlTagCustomControl.UserSuggestedPropertyName} = base.FindByXPath<{oneHtmlTagCustomControl.MappedOOSFWebUIControlName}>(" +
								$"\"{oneHtmlTagCustomControl.HtmlTagInfo.XPathInfo.XPathByDomPath.Replace ("\"", "\\\"")}\", " +
								$"(xPath, webElement, webDriver) => new {oneHtmlTagCustomControl.MappedOOSFWebUIControlName} (webElement, xPath, LocateByWhat.XPath, webDriver)" +
								");"
							)
						);
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

		private CodeTypeMember DefinePropertyOutsideConstructor (UIControlHtmlTagMapperControl oneHtmlTagCustomControl)
		{
			if (oneHtmlTagCustomControl.DoNotInitializeInConstructor == false)
			{
				throw new InvalidOperationException ("This method should only be called for controls that are not initialized in the constructor.");
			}

			var fieldType = oneHtmlTagCustomControl.MappedOOSFWebUIControlName;
			var propertyName = oneHtmlTagCustomControl.UserSuggestedPropertyName;
			var privateFieldName = $"_{char.ToLower (propertyName [0])}{propertyName.Substring (1)}";

			var privateField = new CodeMemberField
			{
				Name = privateFieldName,
				Type = new CodeTypeReference (fieldType),
				Attributes = MemberAttributes.Private
			};

			var property = new CodeMemberProperty
			{
				Name = propertyName,
				Type = new CodeTypeReference (fieldType),
				Attributes = MemberAttributes.Public | MemberAttributes.Final,
				HasGet = true
			};

			if (oneHtmlTagCustomControl.HtmlTagInfo.XPathInfo != null)
			{
				var xPathInfo = oneHtmlTagCustomControl.HtmlTagInfo.XPathInfo;
				var statements = property.GetStatements;

				statements.Add (new CodeCommentStatement (string.Empty));
				statements.Add (new CodeCommentStatement ("Other available X-paths:"));
				statements.Add (new CodeCommentStatement (string.Empty));
				statements.Add (new CodeCommentStatement ($"X-path by 'id' : { xPathInfo.XPathById?.Trim ()}", true));
				statements.Add (new CodeCommentStatement ($"X-path by 'data-testid' : { xPathInfo.XPathByDataTestId?.Trim ()}", true));
				statements.Add (new CodeCommentStatement ($"X-path by 'name' : {xPathInfo.XPathByName?.Trim ()}", true));
				statements.Add (new CodeCommentStatement ($"X-path by 'class' : {xPathInfo.XPathByCssClass?.Trim ()}", true));
				statements.Add (new CodeCommentStatement (string.Empty));
			}

			var assignmentExpression = string.Empty;

			if (fieldType == nameof (RadioButtons))
			{
				assignmentExpression = $"{privateFieldName} = base.{ WebUiPageBase.CodeDomHelper.FindRadioButtonGroupByName }(\"{oneHtmlTagCustomControl.HtmlTagInfo.Name}\");";
			}
			else if (fieldType == nameof (DropDownList))
			{
				assignmentExpression = $"{privateFieldName} = base.{WebUiPageBase.CodeDomHelper.FindDropDownListByXPath}(\"{oneHtmlTagCustomControl.HtmlTagInfo.XPathInfo.XPathByDomPath}\");";
			}
			else if (fieldType == nameof (MultiSelectListBox))
			{
				assignmentExpression = $"{privateFieldName} = base.{WebUiPageBase.CodeDomHelper.FindMultiSelectListBoxByXPath}(\"{oneHtmlTagCustomControl.HtmlTagInfo.XPathInfo.XPathByDomPath}\");";
			}
			else
			{
				assignmentExpression = $"{privateFieldName} = base.FindByXPath<{fieldType}>(\"{oneHtmlTagCustomControl.HtmlTagInfo.XPathInfo.XPathByDomPath.Replace ("\"", "\\\"")}\", (xPath, webElement, webDriver) => new {fieldType}(webElement, xPath, LocateByWhat.XPath, webDriver));";
			}

			property.GetStatements.Add (
				new CodeConditionStatement (
					new CodeBinaryOperatorExpression (
						new CodeVariableReferenceExpression (privateFieldName),
						CodeBinaryOperatorType.IdentityEquality,
						new CodePrimitiveExpression (null)
					),
					[
						new CodeSnippetStatement ($"\t{ assignmentExpression}")
					]));

			property.GetStatements.Add (
				new CodeMethodReturnStatement (
					new CodeVariableReferenceExpression (privateFieldName)
				)
			);

			return property;
		}

		private void quitButton_Click (object sender, EventArgs e)
		{
			if (MessageBox.Show ("Are you sure you want to quit?", "Confirm Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				this.Close ();
			}
		}

		private void LoadLastUsedNamesToTextFields ()
		{
			var iniFilePath = Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "LastUsed.ini");

			if (File.Exists (iniFilePath))
			{
				var lines = File.ReadAllLines (iniFilePath);

				foreach (var line in lines)
				{
					if (line.StartsWith ("Namespace=", StringComparison.OrdinalIgnoreCase))
					{
						this.namespaceTextBox.Text = line.Substring ("Namespace=".Length).Trim ();
					}
					else if (line.StartsWith ("PageModelName=", StringComparison.OrdinalIgnoreCase))
					{
						this.pageModelNameTextBox.Text = line.Substring ("PageModelName=".Length).Trim ();
					}
					else
					{
						break;
					}
				}

				var propertyLines = lines
					.Where (l => l.StartsWith ("PropertyInfo=", StringComparison.OrdinalIgnoreCase))
					.Select (l => l.Substring ("PropertyInfo=".Length).Trim ())
					.ToList ();

				if (propertyLines.Any ())
				{
					var index = 0;
					foreach (var control in this.htmlTagInfoFlowLayoutPanel.Controls.OfType<UIControlHtmlTagMapperControl> ())
					{
						if (index < propertyLines.Count)
						{
							var lineParts = propertyLines [index].Split (':');
							if (lineParts.Length == 2)
							{
								var controlType = lineParts [0].Trim ();
								var propertyName = lineParts [1].Trim ();

								if (control.MappedOOSFWebUIControlName == controlType)
								{
									control.ChangeUserSuggestedPropertyNameTo (propertyName);
								}
								else
								{
									break; // Stop if the control type does not match, as the order should be consistent.
								}
							}
							index++;
						}
						else
						{
							break; // No more property lines to process.
						}
					}
				}
			}
		}
	}
}