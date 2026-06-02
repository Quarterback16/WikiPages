using System;
using System.Collections.Generic;

namespace WikiPages
{
    public class WikiProperties : WikiElement
    {
        public Dictionary<string, string> Properties { get; set; }
            = new Dictionary<string, string>();

        public WikiProperties(
            Dictionary<string,string> props)
        {
            Properties = props;
        }

        public override void Render()
        {
            Console.WriteLine("---");
            foreach (var prop in Properties)
            {
                Console.WriteLine($"{prop.Key} : {prop.Value}");
            }
            Console.WriteLine("---");
        }

        public override string RenderAsString()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("---");
            foreach (var prop in Properties)
            {
                sb.AppendLine($"{prop.Key} : {prop.Value}");
            }
            sb.AppendLine("---");

            return sb.ToString();
        }
    }
}
