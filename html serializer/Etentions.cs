using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace html_serializer
{
    public static class HtmlElementExtensions
    {
        public static List<HtmlElement> QuerySelector(this HtmlElement element, Selector selector)
        {
            List<HtmlElement> results = new List<HtmlElement>();
            QuerySelectorRecursive(element, selector, results);
            return results;
        }

        private static void QuerySelectorRecursive(HtmlElement currentElement, Selector? currentSelector, List<HtmlElement> results)
        {
            if (currentSelector == null)
            {
                results.Add(currentElement);
                return;
            }

            List<HtmlElement> children = currentElement.Descendants().ToList();

            children = children.Where((element) =>
            {
                bool hasId = currentSelector.Id != null ? $"\"{currentSelector.Id}\"" == element.Id : true;
                bool hasName = currentSelector.TagName != null ? currentSelector.TagName == element.Name : true;
                bool hasClasses = currentSelector.Classes != null && element.Classes != null ?
                    currentSelector.Classes.All(selectorClass => element.Classes.Contains($"\"{selectorClass}\"")) : true;
                return hasId && hasName && hasClasses;
            }).ToList();
            foreach (var child in children)
            {
                QuerySelectorRecursive(child, currentSelector.Child, results);
            }
        }
    }
}
