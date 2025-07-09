using System;
using System.Collections.Generic;
using System.Text;

namespace WikiPages
{
    public class WikiUnnumberedChecklist : WikiElement
    {
        public List<string> Tasks { get; set; }
        public string Title { get; set; }

        public WikiUnnumberedChecklist(
                    string title,
                    List<string> tasks)
        {
            Title = title;
            Tasks = tasks;
        }
        public override void Render()
        {
            if (!string.IsNullOrEmpty(Title))
                Console.WriteLine($" * {Title}");
            foreach (var item in Tasks)
                Console.WriteLine($"-  [ ] {item}");
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(Title))
                sb.AppendLine($" * {Title}");
            foreach (var item in Tasks)
            {
                sb.AppendLine($"-  [ ] {item}");
            }
            return sb.ToString();
        }
    }
}
