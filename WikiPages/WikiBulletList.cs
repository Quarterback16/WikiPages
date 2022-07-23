using System;
using System.Text;

namespace WikiPages
{
    public class WikiBulletList : WikiElement
	{
		public string[] Bullets { get; set; }
		public WikiHeading Header { get; set; }

		public WikiBulletList(
			string[] bullets,
			WikiHeading header)
		{
			Type = "BulletList";
			Bullets = bullets;
			Header = header;
		}
		public override void Render()
		{
			if (Header != null)
			{
				Header.Render();
			}
			foreach (var bullet in Bullets)
			{
				Console.WriteLine($" * {bullet}");
			}
			Console.WriteLine();
		}
		public override string ToString()
		{
			var sb = new StringBuilder();
			if (Header != null)
			{
				Header.ToString();
			}
			foreach (var bullet in Bullets)
			{
				sb.AppendLine($" * {bullet}");
			}
			sb.AppendLine();
			return sb.ToString();
		}
	}
}
