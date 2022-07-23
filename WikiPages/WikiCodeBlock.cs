using System;
using System.Collections.Generic;
using System.Text;

namespace WikiPages
{
    public class WikiCodeBlock : WikiElement
	{
		public string CodeType { get; set; }
		public List<string> Lines { get; set; }
		public WikiCodeBlock(
				 string codeType,
				 List<string> lines)
		{
			CodeType = codeType;
			Lines = lines;
		}

		public override void Render()
		{
			Console.WriteLine($"```{CodeType}");
			Lines.ForEach(l => Console.WriteLine(l));
			Console.WriteLine($"```");
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine($"```{CodeType}");
			Lines.ForEach(l => sb.AppendLine(l));
			sb.AppendLine($"```");
			return sb.ToString();
		}
	}
}
