using System;
using System.Text;

namespace WikiPages
{
    public class WikiBullet : WikiElement
	{
		public string Text { get; set; }
		public WikiBullet(
			string text)
		{
			Type = "Bullet";
			Text = text;
		}
		public override void Render()
		{
			Console.WriteLine($" * {Text}");
		}
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine($" * {Text}");
			return sb.ToString();
		}
	}
}
