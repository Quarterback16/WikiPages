using Microsoft.VisualStudio.TestTools.UnitTesting;
using WikiPages;

namespace WikiPage.Tests
{
    [TestClass]
    public class WikiTests
    {
        [TestMethod]
        public void WikiTeables_CanHave_RightJustifiedColumns()
        {
            var testPage = new WikiPages.WikiPage();
            testPage.AddHeading("Test Table");
            var testTable = new WikiTable();
            testTable.AddColumn("Metric Name");
            testTable.AddColumnRight("Metric Value");
            testTable.AddRows(1);
            testTable.AddCell(1, 0, "Everingthing");
            testTable.AddCell(1, 1, "42");
            testPage.AddTable(testTable);
            testPage.RenderToConsole();
        }
    }
}
