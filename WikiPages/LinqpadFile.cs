using System;

namespace WikiPages
{
    public class LinqpadFile
	{
		public string FileName { get; set; }

		public LinqpadFile(string fileName)
		{
			FileName = fileName;
		}
		public string FileOut()
		{
			return $"{Environment.NewLine}* _[[{FileName}.linq]]_";
		}
	}
}
