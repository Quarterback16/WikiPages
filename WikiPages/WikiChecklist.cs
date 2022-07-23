using System;
using System.Collections.Generic;
using System.Text;

namespace WikiPages
{
    public class WikiChecklist : WikiElement
	{
		public List<string> Tasks { get; set; }
		public string Title { get; set; }

		public WikiChecklist(
					string title,
					List<string> tasks)
		{
			Title = title;
			Tasks = tasks;
		}
		public override void Render()
		{
			var taskCount = 0;
			Console.WriteLine($" * {Title}");
			foreach (var item in Tasks)
			{
				Console.WriteLine($"   {++taskCount}. [ ] {item}");
			}
			Console.WriteLine();
		}
		public override string ToString()
		{
			var taskCount = 0;
			var sb = new StringBuilder();
			sb.AppendLine($" * {Title}");
			foreach (var item in Tasks)
			{
				sb.AppendLine($"   {++taskCount}. [ ] {item}");
			}
			sb.AppendLine();
			return sb.ToString();
		}
	}
}
