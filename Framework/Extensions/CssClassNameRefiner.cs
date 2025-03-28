namespace OOSelenium.Framework.Extensions
{
	public static class CssClassNameRefiner
	{
		public static string RefineForButton (this string cssClassName)
		{
			return $"button.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForInputField (this string cssClassName)
		{
			return $"input.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForSpan (this string cssClassName)
		{
			return $"span.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForDiv (this string cssClassName)
		{
			return $"div.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForLabel (this string cssClassName)
		{
			return $"label.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForAnchor (this string cssClassName)
		{
			return $"a.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForTable (this string cssClassName)
		{
			return $"table.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForTableHeader (this string cssClassName)
		{
			return $"th.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForTableRow (this string cssClassName)
		{
			return $"tr.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForTableCell (this string cssClassName)
		{
			return $"td.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForCheckBox (this string cssClassName)
		{
			return $"input.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForRadioButton (this string cssClassName)
		{
			return $"input.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForDropDownList (this string cssClassName)
		{
			return $"select.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForMultiSelectListBox (this string cssClassName)
		{
			return $"select.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForUnorderedList (this string cssClassName)
		{
			return $"ul.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForOrderedList (this string cssClassName)
		{
			return $"ol.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForListItem (this string cssClassName)
		{
			return $"li.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForValidationLabel (this string cssClassName)
		{
			return $"label.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForImage (this string cssClassName)
		{
			return $"img.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForParagraph (this string cssClassName)
		{
			return $"p.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForHeading1 (this string cssClassName)
		{
			return $"h1.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForHeading2 (this string cssClassName)
		{
			return $"h2.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForHeading3 (this string cssClassName)
		{
			return $"h3.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForHeading4 (this string cssClassName)
		{
			return $"h4.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForHeading5 (this string cssClassName)
		{
			return $"h5.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForHeading6 (this string cssClassName)
		{
			return $"h6.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForSection (this string cssClassName)
		{
			return $"section.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForArticle (this string cssClassName)
		{
			return $"article.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForForm (this string cssClassName)
		{
			return $"form.{cssClassName.Replace (" ", ".")}";
		}

		public static string RefineForNav (this string cssClassName)
		{
			return $"nav.{cssClassName.Replace (" ", ".")}";
		}
	}
}