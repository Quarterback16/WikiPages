namespace WikiPages
{
	public class WikiColumnRight : WikiColumn
	{
		public WikiColumnRight(
			   string header) : base(header)
		{
			Header = header;
			RightJustify = true;
		}
	}
}
