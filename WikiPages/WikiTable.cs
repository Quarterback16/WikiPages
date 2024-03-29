﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

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
				sb.Append($" {col.HeaderCode()} |");
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

		public void AddRows(int numberOfRows)
		{
			var existingRows = RowData.Count();
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
				rowContents[col] = cellValue;
				RowData[row] = rowContents;
			}
		}
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
		private int MaxColWidth(int colNumber)
		{
			var max = Column(colNumber).Header.Length;
			for (int r = 1; r < RowData.Count + 1; r++)
			{
				var rowContents = RowData[r];
				var cell = rowContents[colNumber];
				if (cell.Length > max)
					max = cell.Length;
			}
			return max;
		}
		private WikiColumn Column(int colNumber)
		{
			var colNo = 0;
			foreach (var col in Columns)
			{
				colNo++;
				if (colNo.Equals(colNumber))
					return col;
			}
			return new WikiColumn();
		}
	}
}
