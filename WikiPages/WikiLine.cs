using System;
using System.Text;

namespace WikiPages
{
    public class WikiLine : WikiElement
	{
		public string LineText { get; set; }
		public WikiLine(
			string lineText)
		{
			Type = "Line";
			LineText = lineText;
		}
		public override void Render()
		{
			Console.WriteLine(LineText);
		}
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine(LineText);
			return sb.ToString();
		}
	}
}
