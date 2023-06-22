using FluentAssertions;
using Xbehave;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;

using SampleWebApp.UiTests.Entities;
using SampleWebApp.UiTests.UiInteractionSample.Helpers.Components;
using SampleWebApp.UiTests.UiInteractionSample.Helpers.Screens;
using SampleWebApp.UiTests.UiInteractionSample.Helpers;

namespace SampleWebApp.UiTests.Modules.Auth.Tests
{
	public sealed class LetsKodeUiFeature
		: WebUiTestBase
	{
		private readonly ITestBackgroundDataProvider<UserRole, TestEnvironment> dataProvider;
		private readonly LetsKodeUiComponent<UserRole, TestEnvironment> letsKodeComponent;
		private LetsKodeItPage letsKodePage;

		public LetsKodeUiFeature ()
		{
			this.dataProvider = new LetsKodeScreenDataProvider ();
			this.letsKodeComponent
				= new LetsKodeUiComponent<UserRole, TestEnvironment> (dataProvider);
		}

		[Scenario]
		public async void Lets_kode_UI_must_have_Cars_radio_buttons_group_with_correct_options ()
		{
			var carsRadioGroup = (RadioButtons) null;

			"Given that Let's Kode page is available"
				.x (() =>
				{
					letsKodePage = letsKodeComponent.GetLetsKodePage ();
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
					carsRadioGroup.SetRadioTo ("Benz");
				});

			"Then the selected radio button should be of \"Benz\""
				.x (() =>
				{
					carsRadioGroup.SelectedOption.Text.Should ().Be ("Benz");
					carsRadioGroup.SelectedOption.Value.Should ().Be ("benz");
				});
		}

		[Scenario]
		public async void Lets_kode_UI_must_have_Cars_drop_down_list_with_correct_entries ()
		{
			var carsDropDown = (DropDownList) null;

			"Given that Let's Kode page is available"
				.x (() =>
				{
					letsKodePage = letsKodeComponent.GetLetsKodePage ();
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
					var firstEntry = carsDropDown.DropDownOptions [0];
					var secondEntry = carsDropDown.DropDownOptions [1];
					var thirdEntry = carsDropDown.DropDownOptions [2];

					// Assert expectations.
					carsDropDown.DropDownOptions.Count.Should ().Be (3);

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
		public async void Lets_kode_UI_must_have_Fruits_multi_list_box_with_correct_entries ()
		{
			var fruitsMultiListBox = (MultiSelectListBox) null;

			"Given that Let's Kode page is available"
				.x (() =>
				{
					letsKodePage = letsKodeComponent.GetLetsKodePage ();
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
		public async void Lets_kode_UI_must_have_BMW_check_box_with_correct_values ()
		{
			var bmwCheckBox = (CheckBox) null;

			"Given that Let's Kode page is available"
				.x (() =>
				{
					letsKodePage = letsKodeComponent.GetLetsKodePage ();
				});

			"When I check the check-boxes on the page"
				.x (() =>
				{
					bmwCheckBox = this.letsKodePage.BmwCheckBox;
				});

			"Then I should see the check-boxes with the correct values"
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
			this.letsKodeComponent.Dispose ();
			this.letsKodePage.Dispose ();
		}
	}
}