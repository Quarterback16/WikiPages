using System;
using System.Text;

namespace WikiPages
{
    public class WikiBlank : WikiElement
	{
		public WikiBlank()
		{
			Type = "Blank-Line";
		}
		public override void Render()
		{
			Console.WriteLine();
		}
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine();
			return sb.ToString();
		}
	}
}
