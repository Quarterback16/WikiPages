using System.Collections.Generic;

namespace WikiPages
{
    public class WikiNoStrikeChecklist : WikiElement
    {
        public List<string> Tasks { get; set; }
        public string Title { get; set; }

        public WikiNoStrikeChecklist(
                    string title,
                    List<string> tasks)
        {
            Title = title;
            Tasks = tasks;
        }
    }
}
