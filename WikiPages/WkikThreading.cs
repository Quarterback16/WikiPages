using System;
using System.Text;

namespace WikiPages
{
    public class WikiThreading : WikiElement
	{
		public string BackLink { get; set; }
		public string ForwardLink { get; set; }
		public LinqpadFile LinqFile { get; set; }
		public bool Line { get; set; }

		public WikiThreading(
			string backLink,
			string forwardLink,
			string fileName = "",
			bool line = true)
		{
			Type = "Threading";
			BackLink = backLink;
			ForwardLink = forwardLink;
			Line = line;
			if (!string.IsNullOrEmpty(fileName))
				LinqFile = new LinqpadFile(fileName);
		}
		public override void Render()
		{
			if (Line)
			{
				Console.WriteLine();
				Console.WriteLine("---");
			}
			Console.WriteLine($"  ⏪ [[{BackLink}]] ♦ [[{ForwardLink}]] ⏩");
			if (LinqFile != null)
			{
				Console.WriteLine();
				Console.WriteLine(LinqFile.FileOut());
				Console.WriteLine();
			}
		}
		public override string ToString()
		{
			var sb = new StringBuilder();
			if (Line)
			{
				sb.AppendLine();
				sb.AppendLine("---");
			}
			sb.AppendLine($"  ⏪ [[{BackLink}]] ♦ [[{ForwardLink}]] ⏩");
			if (LinqFile != null)
			{
				sb.AppendLine();
				sb.AppendLine(LinqFile.FileOut());
				sb.AppendLine();
			}
			return sb.ToString();
		}
	}
}
