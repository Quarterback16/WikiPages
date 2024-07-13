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
    }
}
