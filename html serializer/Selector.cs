using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace html_serializer
{
    public class Selector
    {
        public string? TagName { get; set; }
        public string? Id { get; set; }
        public List<string>? Classes { get; set; }
        public Selector? Child { get; set; }
        public Selector? Parent { get; set; }

        public Selector()
        {
            Classes = new List<string>();
        }
        public static Selector ConvertToSelector(string query)
        {
            string[] selectors = query.Split(' ');
            Selector root = new Selector();
            Selector currentSelector = root;
            char[] separators = new char[] { '.', '#' };
            for (int selectorIndex = 0; selectorIndex < selectors.Length; selectorIndex++)
            {
                string[] selectorParts = Regex.Split(selectors[selectorIndex], @"(?=[.#])")
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .ToArray();
                for (int i = 0; i < selectorParts.Length; i ++)
                {
                    if (selectorParts[i][0] == '#') currentSelector.Id = selectorParts[i].Substring(1);
                    else if (selectorParts[i][0] == '.') currentSelector.Classes?.Add(selectorParts[i].Substring(1));
                    else if (HtmlHelper.Instance.HtmlTags.Contains(selectorParts[i])) currentSelector.TagName = selectorParts[i];
                }
                if (selectorIndex<selectors.Length-1)
                {
                    Selector newSelector = new Selector();
                    newSelector.Parent = currentSelector;
                    currentSelector.Child = newSelector;
                    currentSelector = newSelector;
                }
            }
            return root;
        }
    }
}
