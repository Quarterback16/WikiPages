using System;
using System.Collections.Generic;
using System.Text;

namespace WikiPages
{
    public class WikiTags : WikiElement
	{
		public string[] _tags { get; set; }
		public List<string> TagLines { get; set; }
		public string Tagline { get; set; }

		public WikiTags(
			string[] tags = null,
			List<string> lines = null)
		{
			Type = "Tags";
			TagLines = new List<string>();
			if (lines != null)
				TagLines.AddRange(lines);
			if (tags != null)
			{
				_tags = tags;
				Tagline = (_tags != null) ? String.Join(",", _tags) : "";
			}
		}
		public void AddTagLine(string line)
		{
			TagLines.Add(line);
		}
		public override void Render()
		{
			Console.WriteLine("---");
			if (!string.IsNullOrEmpty(Tagline))
				Console.WriteLine($"tags: [{Tagline}]");
			foreach (var line in TagLines)
				Console.WriteLine(line);
			Console.WriteLine($"when: {DateUte.StdDate(DateTime.Now)}");
			Console.WriteLine("---");
			Console.WriteLine();
		}
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine("---");
			if (!string.IsNullOrEmpty(Tagline))
				sb.AppendLine($"tags: [{Tagline}]");
			foreach (var line in TagLines)
				sb.AppendLine(line);
			sb.AppendLine($"when: {DateUte.StdDate(DateTime.Now)}");
			sb.AppendLine("---");
			sb.AppendLine();
			return sb.ToString();
		}
	}
}
