namespace WikiPages
{
	public class WikiColumn
	{
		public string Header { get; set; }
		public bool RightJustify { get; set; }
		public bool LeftJustify { get; set; }
		public bool Centred { get; set; }

		public WikiColumn()
		{
			Header = string.Empty;
		}
		public WikiColumn(
			string header)
		{
			Header = header;
		}
		public virtual string HeaderCode(int width)
        {
			if (Centred)
				return $":{new string('-', width)}:";
			return new string('-',width+2);
        }
	}
}
