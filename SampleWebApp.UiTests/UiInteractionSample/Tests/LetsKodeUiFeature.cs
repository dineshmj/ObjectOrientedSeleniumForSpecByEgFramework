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
			this.letsKodeComponent = new LetsKodeUiComponent<UserRole, TestEnvironment> (dataProvider);
		}

		[Scenario]
		public async void Lets_kode_UI_must_have_Cars_radio_buttons_group ()
		{
			var carsRadioGroup = (RadioButtons) null;

			"Given that Let's Kode page is available"
				.x (() =>
				{
					letsKodePage = letsKodeComponent.GetLetsKodePage ();
				});

			"When I check the page title, labels of UI fileds, and text of buttons"
				.x (() =>
				{
					// Read the UI field labels.
					carsRadioGroup = letsKodePage.CarRadioButtonGroup;
				});

			"Then the page title, labels and texts should be as expected"
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

			"When I click the second radio option in the group"
				.x (() =>
				{
					carsRadioGroup.SetRadioTo ("Benz");
				});

			"Then the selected radio option should be as expected"
				.x (() =>
				{
					carsRadioGroup.SelectedOption.Text.Should ().Be ("Benz");
					carsRadioGroup.SelectedOption.Value.Should ().Be ("benz");
				});
		}
	}
}
