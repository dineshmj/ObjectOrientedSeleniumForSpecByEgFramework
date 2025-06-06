﻿using System.Reflection;

using OpenQA.Selenium;

namespace OOSelenium.Framework.Abstractions
{
	public abstract class WebUiTestBase
		: IDisposable
	{
		public virtual void Dispose ()
		{
			this
				.GetType ()
				.GetMembers (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)  // Get all members.
				.Where (m => m.MemberType == MemberTypes.Field) // Get the private fields.
				.ToList ()
				.ForEach (field => {
					var privateField = (FieldInfo) field;

					// Is the private instance field of the inheriting child class disposable?
					if (privateField != null && typeof (IWebDriver).IsAssignableFrom (privateField.FieldType))
					{
						var webDriver = privateField.GetValue (this) as IWebDriver;
						webDriver?.Quit ();
					}

					if (privateField != null && typeof (IDisposable).IsAssignableFrom (privateField.FieldType))
					{
						var disposable = privateField.GetValue (this) as IDisposable;
						disposable?.Dispose ();
					}
				});
		}
	}
}