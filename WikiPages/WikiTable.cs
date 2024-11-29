using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WikiPages
{
    public class WikiTable : WikiElement
	{
		public List<WikiColumn> Columns { get; set; }
		public Dictionary<int, string[]> RowData { get; set; }
		public WikiTable()
		{
			Columns = new List<WikiColumn>();
			RowData = new Dictionary<int, string[]>();
		}

        public void AddCellData<T>(
            IEnumerable<WikiColumn> cols,
            List<Func<T, string>> colFns,
            IEnumerable<T> tips,
            WikiTable t)
        {
            t.Columns = cols.ToList();
            t.AddRows(tips.Count());
            foreach (var (tip, r) in tips.Select((tip, r) => (tip, r)))
            {
                foreach (var (colFn, c) in colFns.Select((colFn, c) => (colFn, c)))
                {
                    t.AddCell(r + 1, c, colFn(tip));
                }
            }
        }

        public override void Render()
		{
			RenderHeader();
			int rowNumber = 0;
			foreach (var row in RowData)
			{
				rowNumber++;
				RenderRow(rowNumber);
			}
			Console.WriteLine();
		}
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine(
				RenderHeader(false));
			int rowNumber = 0;
			foreach (var row in RowData)
			{
				rowNumber++;
				sb.AppendLine(
					RenderRow(
						rowNumber,
						false));
			}
			return sb.ToString();
		}

        public string ToHtml()
        {
            var sb = new StringBuilder();
            sb.AppendLine(
               "<table border='1'>");
            sb.AppendLine(RenderTableHeader());
            int rowNumber = 0;
            foreach (var row in RowData)
            {
                rowNumber++;
                sb.AppendLine(RenderRowToHtml(rowNumber));
            }
            sb.AppendLine("</table>");
            return sb.ToString();
        }

        private string RenderTableHeader()
        {
            var sb = new StringBuilder();
            foreach (var col in Columns)
            {
                if (col.Header.Length > 0)
                    col.Header = $"{col.Header}";
                sb.AppendLine($"   <th> {col.Header} </th>");
            }
            return sb.ToString();
        }

        private string RenderRowToHtml(
           int rowNumber)
        {
            var sb = new StringBuilder();
            sb.AppendLine("   <tr>");
            var rowData = RowData[rowNumber];
            var colNumber = 0;
            foreach (var col in Columns)
            {
                var cellValue = rowData[colNumber] ?? string.Empty;
                sb.AppendLine($"      <td> {cellValue} </td>");
                colNumber++;
            }
            sb.AppendLine("   </tr>");
            return sb.ToString();
        }

        public void ToCsv(
        string fileName)
        {
            using (StreamWriter outputFile = new StreamWriter(fileName))
            {
                outputFile.WriteLine(CsvHeader());
                RowData.ToList().ForEach(r => outputFile.WriteLine(CsvRow(r)));
            }
        }
        private string CsvRow(KeyValuePair<int, string[]> r) =>
           string.Join(",", r.Value.Select(c => $"\"{c.Trim()}\""));

        private string CsvHeader() =>
           string.Join(",", Columns.Select(c => $"\"{c.Header}\""));

        private string RenderRow(
			int rowNumber,
			bool toConsole = true)
		{
			var sb = new StringBuilder();
			sb.Append("|");
			var rowData = RowData[rowNumber];
			var colNumber = 0;
			foreach (var col in Columns)
			{
				var cellValue = rowData[colNumber] ?? string.Empty;
				sb.AppendFormat("  {0}  |", cellValue.PadRight(MaxColWidth(col)));
				colNumber++;
			}
			if (toConsole)
				Console.WriteLine(sb.ToString());
			return sb.ToString();
		}
        private string RenderHeader(
            bool toConsole = true)
        {
            var sb = new StringBuilder();
            sb.Append("|");
            foreach (var col in Columns)
            {
                if (col.Header.Length > 0)
                    col.Header = $"{col.Header}";
                sb.AppendFormat("  {0}  |", col.Header.PadRight(MaxColWidth(col)));
            }
            sb.AppendLine();
            sb.Append("|");
            foreach (var col in Columns)
            {
                sb.Append(col.RightJustify
                    ? $" {new string('-', Math.Max(MaxColWidth(col), col.Header.Length) + 1)}: |"
                    : $" {new string('-', Math.Max(MaxColWidth(col), col.Header.Length) + 2)} |");
            }
            if (toConsole)
                Console.WriteLine(sb.ToString());
            return sb.ToString();
        }
        public WikiTable AddColumn(
			string header)
		{
			Columns.Add(new WikiColumn(header));
			return this;
		}

		public WikiTable AddColumnRight(
			string header)
		{
			Columns.Add(new WikiColumnRight(header));
			return this;
		}

        public WikiTable AddColumnCentred(
            string header)
        {
            Columns.Add(new WikiColumnCentre(header));
            return this;
        }

        public WikiTable AddColumns(
            string[] headers)
        {
            headers.ToList()
                .ForEach(c =>
                    Columns.Add(new WikiColumn(c)));
            return this;
        }

        public WikiTable AddColumns(
            List<WikiColumn> cols)
        {
            cols.ForEach(c =>
                    Columns.Add(c));
            return this;
        }

        public WikiTable AddColumnsRight(
            string[] headers)
        {
            headers.ToList()
                .ForEach(c =>
                    Columns.Add(new WikiColumnRight(c)));
            return this;
        }

        public void AddRows(int numberOfRows)
		{
			var existingRows = RowData.Count;
			for (int i = 1; i < numberOfRows + 1; i++)
			{
				var arr = new string[Columns.Count];
				for (int j = 0; j < Columns.Count; j++)
				{
					arr[j] = string.Empty;
				}
				RowData.Add(existingRows + i, arr);
			}
		}
		public void AddRow(int nRow, string[] content)
		{
			for (int i = 0; i < content.Length; i++)
			{
				AddCell(nRow, i, content[i]);
			}
		}
		public void AddCell(
			int row,
			int col,
			string cellValue)
		{
			if (RowData.ContainsKey(row))
			{
				var rowContents = RowData[row];
				if (col <= rowContents.Length - 1)
				{
					rowContents[col] = cellValue;
				}
				RowData[row] = rowContents;
			}
		}
        public void AddCell(
            int row,
            string colName,
            string cellValue)
        {
            var rowContents = RowData[row];
            rowContents[FindColumnIndex(colName)] = cellValue;
            RowData[row] = rowContents;
        }

        public int FindColumnIndex(string nameToFind) =>
            Columns.FindIndex(col => col.Header == nameToFind);

        private int MaxColWidth(WikiColumn col)
		{
			var max = col.Header.Length;
			var colNumber = ColumnNumber(col);
			for (int r = 1; r < RowData.Count + 1; r++)
			{
				var rowContents = RowData[r];
				var cell = rowContents[colNumber - 1];
				if (!string.IsNullOrEmpty(cell))
				{
					if (cell.Length > max)
						max = cell.Length;
				}
			}
			return max;
		}
		private int ColumnNumber(WikiColumn col)
		{
			var colNumber = 0;
			foreach (var column in Columns)
			{
				colNumber++;
				if (col.Equals(column))
				{
					return colNumber;
				}
			}
			return 1;
		}

	}
}
