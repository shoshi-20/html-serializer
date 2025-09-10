// See https://aka.ms/new-console-template for more information
using html_serializer;
using System.Text.RegularExpressions;

async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}
//var cleanHtml = new Regex("\\s").Replace(html,"");
var html = await Load("http://hebrewbooks.org/beis");

var cleanHtml = new Regex("(?<=^|>)[\\s]+(?=<|$)|[\\r\\n\\t]").Replace(html, "");

var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(s => s.Length > 0);

HtmlElement root = new HtmlElement();
HtmlElement currentElement = root;
htmlLines = htmlLines.Skip(1).ToArray();

foreach (var line in htmlLines)
{
    var firstWord=line.Split(' ')[0];
    if (firstWord.Length > 0)
    {
        if (firstWord == "/html") break;
        else if (firstWord[0] == '/')
            currentElement = currentElement.Parent;
        else if (HtmlHelper.Instance.HtmlTags.Contains(firstWord))
        {
            HtmlElement newElement = new HtmlElement();
            newElement.Parent = currentElement;
            newElement.Name = firstWord;
            currentElement.Children.Add(newElement);
            var attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(line).Cast<Match>().Select(m => m.Value).ToList();
            newElement.Attributes = attributes;
            foreach (var attribute in attributes)
            {
                int equal = attribute.IndexOf('=');
                string attributeName = attribute.Substring(0, equal);
                if (attributeName == "class")
                    newElement.Classes = attribute.Substring(equal + 1).Split(" ").ToList();
                else if (attributeName == "id")
                    newElement.Id = attribute.Substring(equal + 1);
            }
            if (!(line[line.Length - 1] == '/' || HtmlHelper.Instance.HtmlVoidTags.Contains(firstWord)))
                currentElement = newElement;
        }
        else currentElement.InnerHtml = line;
    }
}
root = root.Children[0];
string userSelector = "#pnlMenubar td .inactBG";
//string userSelector = "tr.oddrow";
//string userSelector = "#mshead";

//string userSelector = "#cpMstr_PanelSeforim";
//string userSelector = "img#imgMainLogo";
var selector = Selector.ConvertToSelector(userSelector);
List<HtmlElement> matchingElements = root.QuerySelector(selector);
var hashedResult = new HashSet<HtmlElement>(matchingElements);
foreach (var element in hashedResult) { Console.WriteLine(element.ToString()); }
Console.ReadLine();

