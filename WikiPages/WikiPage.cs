using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WikiPages
{
    public class WikiPage
	{
		public List<WikiElement> Elements { get; set; }
		public string Title { get; set; }
		public WikiPage()
		{
			Elements = new List<WikiElement>();
		}
		public WikiPage(string title)
		{
			Elements = new List<WikiElement>();
			Title = title;
		}
		public WikiPage AddElement(
			WikiElement element)
		{
			Elements.Add(element);
			return this;
		}
		public WikiPage AddHeading(
			string heading)
		{
			AddElement(
				new WikiHeading(
					level: 1,
					heading));
			return this;

		}
		public WikiPage AddTable(
			WikiTable table)
		{
			Elements.Add(table);
			return this;
		}
		public WikiPage AddHeading(
			string heading,
			int level)
		{
			AddElement(
				new WikiHeading(
					level: level,
					heading));
			return this;
		}
		public WikiPage AddTags(
			string[] tags = null,
			List<string> lines = null)
		{
			AddElement(
				new WikiTags(tags, lines));
			return this;
		}
		public WikiPage AddBulletedList(
			string title,
			string[] bullets)
		{
			AddElement(
				new WikiHeading(
					level: 2,
					title));
			foreach (var element in bullets)
			{
				AddElement(
					new WikiBullet(
						element));
			}
			return this;
		}
		public WikiPage AddBlankLine()
		{
			AddElement(
				new WikiBlank());
			return this;
		}
		public WikiPage AddLine(string text)
		{
			AddElement(
				new WikiLine(text));
			return this;
		}
		public WikiPage AddHorizontalRule()
		{
			AddElement(
				new WikiHorizontalRule());
			return this;
		}
		public WikiPage AddFileName(string fileName)
		{
			AddElement(
				new WikiFileName(fileName));
			return this;
		}
		public WikiPage AddCodeBlock(string codeType, List<string> lines)
		{
			AddElement(
				  new WikiCodeBlock(codeType, lines));
			return this;
		}
		public WikiPage AddThreading(
			string backLink,
			string forwardLink,
			string fileName = "")
		{
			AddElement(
				new WikiThreading(
					backLink,
					forwardLink,
					fileName,
					false));
			return this;
		}
		public WikiPage AddChecklist(
			string title,
			List<string> tasks)
		{
			AddElement(
				new WikiChecklist(
					title,
					tasks));
			return this;
		}

		public WikiPage AddSection(
			string header,
			string[] list,
			int level = 2)
		{
			var hints = new WikiNumberedList(
				list,
				new WikiHeading(level, header),
				level);
			this.AddElement(hints);
			this.AddElement(new WikiBlank());
			return this;
		}
		public WikiPage AddPreSection(string[] lines)
		{
			this.AddElement(new WikiBlank());
			this.AddElement(new WikiLine("```"));
			lines.ToList().ForEach(l => this.AddElement(new WikiLine(l)));
			this.AddElement(new WikiLine("```"));
			this.AddElement(new WikiBlank());
			return this;
		}

        public WikiPage RenderToConsole()
		{
			foreach (var element in Elements)
			{
				element.Render();
			}
			return this;
		}

        public WikiPage RenderToMarkdownFile(
			string folder = "")
		{
			var pageContents = PageContents();
			// determine title from H1
			Title = DetermineTitle();
			string filePath;
			if (string.IsNullOrEmpty(folder))
				filePath = $"d:\\Dropbox\\Obsidian\\ChestOfNotes\\{Title}.md";
			else
				filePath = $"d:\\Dropbox\\Obsidian\\ChestOfNotes\\{folder}\\{Title}.md";

			using (StreamWriter outputFile = new StreamWriter(
				filePath))
			{
				outputFile.WriteLine(pageContents);
			}
			return this;
		}
		public WikiPage RenderToObsidian(
			string fileName) =>

			RenderToObsidian(
				fileName,
				"d:\\Dropbox\\Obsidian\\ChestOfNotes\\");

        public WikiPage RenderToObsidian(
            string fileName,
			string folder)
        {
            var pageContents = PageContents();
            var filePath
                = $"{folder}{fileName}.md";

            using (StreamWriter outputFile = new StreamWriter(
                filePath))
            {
                outputFile.WriteLine(pageContents);
            }
            return this;
        }
        public string PageContents()
		{
			var sb = new StringBuilder();
			foreach (var element in Elements)
			{
				sb.Append(element.ToString());
			}
			return sb.ToString();
		}

		private string DetermineTitle()
		{
			var title = string.Empty;
			foreach (var element in Elements)
			{
				if (element.Type == "Heading")
				{
					title = element.Value;
				}
			}
			return title;
		}

    }
}
