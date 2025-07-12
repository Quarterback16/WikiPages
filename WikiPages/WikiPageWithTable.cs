namespace WikiPages
{
    public class WikiPageWithTable : WikiPage
    {
        public WikiPage Page { get; set; }
        public WikiTable Table { get; set; }
        public WikiPageWithTable()
        {
            Page = new WikiPage();
            Table = new WikiTable();
        }

        public string PageTableContents()
        {
            if (Table.Columns.Count > 0)
                Elements.Add(Table);
            Page.AddBlankLine();
            return PageContents();
        }
    }
}
