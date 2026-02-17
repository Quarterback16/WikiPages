using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WikiPages;

namespace WikiPage.Tests
{
    [TestClass]
    public class WikiTests
    {
        [TestMethod]
        public void WikiTables_CanHave_RightJustifiedColumns()
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

        [TestMethod]
        public void WikiPageWithTable_DoesntHaveExtraChars()
        {
            var page = new WikiPageWithTable();
            page.AddHeading("ADP Position Rankings : RB");
            var result = page.PageTableContents();
            Console.WriteLine(result);
            Assert.IsFalse(
                result.Contains("|"), 
                "Page should not contain '|' characters.");
        }

        [TestMethod]
        public void WikiTags_CanHave_NoWhen()
        {
            var testPage = new WikiPages.WikiPage();
            testPage.AddTagsNoWhen(tags: new string[] { "tag1", "tag2"});
            testPage.AddHeading("Test Tags");
            testPage.RenderToConsole();
        }

        [TestMethod]
        public void WikiTags_CanHave_When()
        {
            var testPage = new WikiPages.WikiPage();
            testPage.AddTags(tags: new string[] { "tag1", "tag2" });
            testPage.AddHeading("Test Tags");
            testPage.RenderToConsole();
        }
    }
}
