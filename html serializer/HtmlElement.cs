using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace html_serializer
{
    public class HtmlElement
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public List<string>? Attributes { get; set; }
        public List<string>? Classes { get; set; }
        public string? InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }

        public HtmlElement()
        {
            Attributes = new List<string>();
            Classes = new List<string>();
            Children = new List<HtmlElement>();
        }

        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> q = new Queue<HtmlElement>();
            q.Enqueue(this);
            while (q.Count > 0)
            {
                HtmlElement currentElement = q.Dequeue();
                yield return currentElement;
                foreach (HtmlElement child in currentElement.Children)
                {
                    q.Enqueue(child);
                }
            }
        }
        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement currentElement = this;
            while (currentElement.Parent != null)
            {
                yield return currentElement;
                currentElement = currentElement.Parent;
            }
        }
        public override string ToString()
        {
            string classes = "";
            if (this.Classes?.Count > 0)
            {
                foreach (string item in this.Classes)
                {
                    classes += item;
                }
            }
            return $"< {this.Name} id = {this.Id} classes = {classes}  innerHtml = \"{this.InnerHtml}\" >";
        }
    }
}
