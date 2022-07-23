using System;
using System.Text;

namespace WikiPages
{
    public class WikiFileName : WikiElement
	{
		public string FileName { get; set; }
		public WikiFileName(string fileName)
		{
			FileName = fileName;
		}
		public override void Render()
		{
			Console.WriteLine($"  _[[{FileName}]]_");
		}
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine($"  _[[{FileName}]]_");
			return sb.ToString();
		}
	}
}
