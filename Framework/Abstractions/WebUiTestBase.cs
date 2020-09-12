using System;
using System.Linq;
using System.Reflection;

namespace OOSelenium.Framework.Abstractions
{
	public abstract class WebUiTestBase
	{
		~WebUiTestBase ()
		{
			// Dispose all "IDisposable" instance fields of the Web UI tests.
			this
				.GetType ()
				.GetMembers (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)	// Get all members.
				.Where (m => m.MemberType == MemberTypes.Field)	// Get the private fields.
				.ToList ()
				.ForEach (field =>
					{
						var privateField = (FieldInfo) field;

						// Is the private field disposable?
						if (typeof (IDisposable).IsAssignableFrom (privateField.FieldType))
						{
							var disp = privateField.GetValue (this) as IDisposable;
							disp.Dispose ();
						}
					});
		}
	}
}
