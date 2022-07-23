namespace WikiPages
{
    public class WikiColumnLeft : WikiColumn
    {
		public WikiColumnLeft(
			   string header) : base(header)
		{
			Header = header;
			LeftJustify = true;
		}
	}
}
