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

        public override string HeaderCode(int width) =>

            $"{new string('-', width+1)}:";

    }
}
