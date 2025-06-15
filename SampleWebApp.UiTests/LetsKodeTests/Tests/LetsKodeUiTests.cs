using FluentAssertions;
using Xbehave;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;
using SampleWebApp.UiTests.LoginTests.Background;
using SampleWebApp.UiTests.LetsKodeTests.Navigation;
using SampleWebApp.UiTests.LetsKodeTests.Pages;
using SampleWebApp.UiTests.LetsKodeTests.Background;

namespace SampleWebApp.UiTests.LetsKodeTests.Tests
{
	public sealed class LetsKodeUiTests
		: WebUiTestBase
	{
		private readonly IExecutionEnvironmentPageDataProvider<UserRole, ExecutionEnvironment> letsKodeDataProvider;
		private readonly LetsKodeNavigationComponent<UserRole, ExecutionEnvironment> letsKodeNavigationComponent;
		private LetsKodeItPage letsKodePage;

		public LetsKodeUiTests ()
		{
			letsKodeDataProvider = new LetsKodeDataProvider ();
			letsKodeNavigationComponent
				= new LetsKodeNavigationComponent<UserRole, ExecutionEnvironment> (letsKodeDataProvider);
		}

		[Scenario]
		public async void Cars_RadioButtonGroup_MustHave_CorrectEntries ()
		{
			var carsRadioGroup = (RadioButtons) null;

			"Given that Let's Kode page is available"
				.x (() =>
				{
					letsKodePage = letsKodeNavigationComponent.GoToLetsKodePage ();
				});

			"When I check the page's Cars radio buttons group"
				.x (() =>
				{
					carsRadioGroup = letsKodePage.CarRadioButtonGroup;
				});

			"Then the Cars radio button group must have expected entries"
				.x (() =>
				{
					var firstRadio = carsRadioGroup.RadioOptions [0];
					var secondRadio = carsRadioGroup.RadioOptions [1];
					var thirdRadio = carsRadioGroup.RadioOptions [2];

					// Assert expectations.
					carsRadioGroup.RadioOptions.Count.Should ().Be (3);

					// Expected radio entries and their order.
					firstRadio.Text.Should ().Be ("BMW");
					firstRadio.Value.Should ().Be ("bmw");
					secondRadio.Text.Should ().Be ("Benz");
					secondRadio.Value.Should ().Be ("benz");
					thirdRadio.Text.Should ().Be ("Honda");
					thirdRadio.Value.Should ().Be ("honda");
				});

			"When I click the second radio option in the Cars radio button group"
				.x (() =>
				{
					carsRadioGroup.SelectRadio ("Benz");
				});

			"Then the selected radio button should be of \"Benz\""
				.x (() =>
				{
					carsRadioGroup.SelectedRadio.Text.Should ().Be ("Benz");
					carsRadioGroup.SelectedRadio.Value.Should ().Be ("benz");
				});
		}

		[Scenario]
		public async void Cars_DropDown_MustHave_CorrectEntries ()
		{
			var carsDropDown = (DropDownList) null;

			"Given that Let's Kode page is available"
				.x (() =>
				{
					letsKodePage = letsKodeNavigationComponent.GoToLetsKodePage ();
				});

			"When I check the page's Cars drop-down list"
				.x (() =>
				{
					// Read the UI field labels.
					carsDropDown = letsKodePage.CarDropDown;
				});

			"Then the Cars drop-down list must have expected entries"
				.x (() =>
				{
					var firstEntry = carsDropDown.DropDownEntries [0];
					var secondEntry = carsDropDown.DropDownEntries [1];
					var thirdEntry = carsDropDown.DropDownEntries [2];

					// Assert expectations.
					carsDropDown.DropDownEntries.Count.Should ().Be (3);

					// Expected radio entries and their order.
					firstEntry.Text.Should ().Be ("BMW");
					firstEntry.Value.Should ().Be ("bmw");
					secondEntry.Text.Should ().Be ("Benz");
					secondEntry.Value.Should ().Be ("benz");
					thirdEntry.Text.Should ().Be ("Honda");
					thirdEntry.Value.Should ().Be ("honda");
				});

			"When I select the second entry in the Cars drop-down list"
				.x (() =>
				{
					carsDropDown.SetSelectionTo ("Benz");
				});

			"Then the selected entry should be of \"Benz\""
				.x (() =>
				{
					carsDropDown.SelectedEntry.Text.Should ().Be ("Benz");
					carsDropDown.SelectedEntry.Value.Should ().Be ("benz");
				});
		}

		[Scenario]
		public async void Fruits_MultiListBox_MustHave_CorrectEntries ()
		{
			var fruitsMultiListBox = (MultiSelectListBox) null;

			"Given that Let's Kode page is available"
				.x (() =>
				{
					letsKodePage = letsKodeNavigationComponent.GoToLetsKodePage ();
				});

			"When I check the page's Fruits multi-select list box"
				.x (() =>
				{
					// Read the UI field labels.
					fruitsMultiListBox = letsKodePage.FruitsMultiListBox;
				});

			"Then the Fruits multi-select list box must have expected entries"
				.x (() =>
				{
					var firstEntry = fruitsMultiListBox.ListEntries [0];
					var secondEntry = fruitsMultiListBox.ListEntries [1];
					var thirdEntry = fruitsMultiListBox.ListEntries [2];

					// Assert expectations.
					fruitsMultiListBox.ListEntries.Count.Should ().Be (3);

					// Expected radio entries and their order.
					firstEntry.Text.Should ().Be ("Apple");
					firstEntry.Value.Should ().Be ("apple");
					secondEntry.Text.Should ().Be ("Orange");
					secondEntry.Value.Should ().Be ("orange");
					thirdEntry.Text.Should ().Be ("Peach");
					thirdEntry.Value.Should ().Be ("peach");
				});

			"When I select both first and third entries in the Fruits multi-select list box"
				.x (() =>
				{
					fruitsMultiListBox.SelectTheseValues ("Apple", "Peach");
				});

			"Then the selected entries should be both \"Apple\" and \"Peach\""
				.x (() =>
				{
					var selectedEntries = fruitsMultiListBox.SelectedEntries;

					var firstSelectedEntry = selectedEntries [0];
					var secondSelectedEntry = selectedEntries [1];

					firstSelectedEntry.Text.Should ().Be ("Apple");
					firstSelectedEntry.Value.Should ().Be ("apple");

					secondSelectedEntry.Text.Should ().Be ("Peach");
					secondSelectedEntry.Value.Should ().Be ("peach");
				});
		}

		[Scenario]
		public async void BMW_CheckBox_MustHaveCorrect_TextAndValue ()
		{
			var bmwCheckBox = (CheckBox) null;

			"Given that Let's Kode page is available"
				.x (() =>
				{
					letsKodePage = letsKodeNavigationComponent.GoToLetsKodePage ();
				});

			"When I check the check-boxes on the page"
				.x (() =>
				{
					bmwCheckBox = letsKodePage.BmwCheckBox;
				});

			"Then I should see the check-boxes with the correct texts"
				.x (() =>
				{
					bmwCheckBox.Text.Should ().Be ("BMW");
					bmwCheckBox.Value.Should ().Be ("bmw");
					bmwCheckBox.IsChecked.Should ().BeFalse ();
				});

			"When I check the BMW check box"
				.x (() =>
				{
					bmwCheckBox.ToggleCheckState ();
				});

			"Then I should see that the check-boxes are checked"
				.x (() =>
				{
					bmwCheckBox.IsChecked.Should ().BeTrue ();
				});
		}

		public override void Dispose ()
		{
			letsKodeNavigationComponent.Dispose ();
			letsKodePage.Dispose ();
		}
	}
}