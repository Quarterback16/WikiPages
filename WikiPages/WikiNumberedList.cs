using System;
using System.Text;

namespace WikiPages
{
    public class WikiNumberedList : WikiElement
	{
		public string[] Bullets { get; set; }
		public WikiHeading Header { get; set; }
		public int Level { get; set; }

		public WikiNumberedList(
			string[] bullets,
			WikiHeading header,
			int level = 2)
		{
			Type = "NumberedList";
			Bullets = bullets;
			Header = header;
			Level = level;
		}
		public override void Render()
		{
			if (Header != null)
			{
				Header.Render();
			}
			var nBullet = 0;
			foreach (var bullet in Bullets)
			{
				Console.WriteLine(LineOut(++nBullet, bullet));
			}
			Console.WriteLine();
		}

		string PadOut(int level)
		{
			if (level == 2)
				return "";
			return "  ";
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			if (Header != null)
			{
				sb.AppendLine(Header.ToString());
			}
			var nBullet = 0;
			foreach (var bullet in Bullets)
			{
				sb.AppendLine(LineOut(++nBullet, bullet));
			}
			sb.AppendLine();
			return sb.ToString();
		}

		string LineOut(int nmbr, string bullet)
		{
			return $"{nmbr}. {bullet}";
		}
	}
}
