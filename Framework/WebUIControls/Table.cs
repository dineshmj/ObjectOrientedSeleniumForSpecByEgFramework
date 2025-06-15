using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class Table
		: WebUiControlBase
	{
		public Table (IWebElement element, string id, IWebDriver webDriver)
			: base (element, id, webDriver)
		{
			if (element.TagName.ToLower () != "table")
			{
				throw new ArgumentException ("The provided element is not a <table> tag.", nameof (element));
			}
		}

		public string? Name
		{
			get { return base.remoteElement.GetAttribute ("name"); }
		}

		public int RowCount
		{
			get
			{
				var rows = base.remoteElement.FindElements (By.TagName ("tr"));
				return rows.Count;
			}
		}

		public int ColumnCount
		{
			get
			{
				var firstRow = base.remoteElement.FindElement (By.TagName ("tr"));
				var cells = firstRow.FindElements (By.TagName ("td"));
				return cells.Count;
			}
		}

		public IWebElement GetCell (int rowIndex, int columnIndex)
		{
			if (rowIndex < 0 || columnIndex < 0)
			{
				throw new ArgumentOutOfRangeException ("Row and column indices must be non-negative.");
			}

			var rows = base.remoteElement.FindElements (By.TagName ("tr"));
			
			if (rowIndex >= rows.Count)
			{
				throw new ArgumentOutOfRangeException ("Row index exceeds the number of rows in the table.");
			}
			
			var cells = rows [rowIndex].FindElements (By.TagName ("td"));
			
			if (columnIndex >= cells.Count)
			{
				throw new ArgumentOutOfRangeException ("Column index exceeds the number of columns in the specified row.");
			}
			
			return cells [columnIndex];
		}

		public string GetTableHeaderRowText (int rowIndex)
		{
			if (rowIndex < 0)
			{
				throw new ArgumentOutOfRangeException ("Row index must be non-negative.");
			}

			var headerRows = base.remoteElement.FindElements (By.TagName ("th"));

			if (rowIndex >= headerRows.Count)
			{
				throw new ArgumentOutOfRangeException ("Row index exceeds the number of header rows in the table.");
			}

			return headerRows [rowIndex].Text;
		}

		public string? GetCellText (int rowIndex, int columnIndex)
		{
			var cell = this.GetCell (rowIndex, columnIndex);
			return cell.Text;
		}

		public void ClickCell (int rowIndex, int columnIndex)
		{
			var cell = this.GetCell (rowIndex, columnIndex);
			cell.Click ();
		}

		public bool HasHeaderRow
		{
			get
			{
				var headerRows = base.remoteElement.FindElements (By.TagName ("th"));
				return headerRows.Count > 0;
			}
		}
	}
}