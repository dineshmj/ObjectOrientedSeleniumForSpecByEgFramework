using System.Collections.Generic;
using System.Collections.ObjectModel;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;
using OOSelenium.Framework.Extensions;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class RadioButtons
		: WebUiControlBase
	{
		private readonly ReadOnlyCollection<IWebElement> radioTags;
		private readonly IList<TextValuePair> radioEntries;

		public RadioButtons (ReadOnlyCollection<IWebElement> radioTags, string id)
			: base ( ((RemoteWebElement) radioTags [0]), id)
		{
			// The radio tags collection.
			this.radioTags = radioTags;

			// The radio options, their texts, and the corresponding values.
			this.radioEntries = new List<TextValuePair> ();

			foreach (var oneRadio in this.radioTags)
			{
				this.radioEntries.Add (new TextValuePair(oneRadio.GetInnerText (this.webDriver, this.id), oneRadio.GetAttribute ("value")));
			}
		}

		public IList<TextValuePair> RadioOptions
		{
			get 
			{
				return new ReadOnlyCollection<TextValuePair> (this.radioEntries);
			}
		}

		public TextValuePair SelectedOption
		{
			get
			{
				foreach (var oneTag in this.radioTags)
				{
					if (oneTag.GetAttribute ("checked") == "true")
						return new TextValuePair (oneTag.GetInnerText (this.webDriver, this.id), oneTag.GetAttribute ("value"));
				}

				return null;
			}
		}

		public void SetRadioTo (string selectedRadioText)
		{
			foreach (var oneTag in this.radioTags)
			{
				if (oneTag.GetInnerText (this.webDriver, this.id) == selectedRadioText)
					oneTag.Click ();
			}
		}
	}
}
