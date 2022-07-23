using System;
using System.Text;

namespace WikiPages
{
    public class WikiHeading : WikiElement
	{
		public int Level { get; set; }
		public string HeaderText { get; set; }
		public WikiHeading(
			int level,
			string header)
		{
			Type = "Heading";
			HeaderText = header;
			Level = level;
			Value = header;
		}
		public override void Render()
		{
			Console.WriteLine($"{HeaderLevel(Level)} {HeaderText}");
			Console.WriteLine();
		}
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine($"{HeaderLevel(Level)} {HeaderText}");
			//sb.AppendLine();
			return sb.ToString();
		}

		private string HeaderLevel(
			int level)
		{

			if (level == 1)
				return "#";
			if (level == 2)
				return "##";
			if (level == 3)
				return "###";
			return "####";
		}
	}
}
