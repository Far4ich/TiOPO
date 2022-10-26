using HtmlAgilityPack;
using System.Net;
using System.Text.RegularExpressions;

const string HREF_PATTERN = @"href\s*=\s*(?:""(?<1>[^""]*)""|(?<1>\S+))";
const string DOMAIN = "http://links.qatl.ru/";//https://koptelnya.ru https://statler.ru https://www.usa.gov https://www.travelline.ru http://links.qatl.ru/

Tuple<string, Link> main;
List<string> links;
Dictionary<string, Link> visitedLinks;

main = GetHtmlDoc(DOMAIN);
links = GetInternalLinks(main.Item2.Doc);
visitedLinks = new();
visitedLinks.Add(DOMAIN, main.Item2);

FindAllInternalLinks(links, visitedLinks);

PrintLinks(visitedLinks);

List<string> GetInternalLinks(HtmlDocument document)
{
    List<string> getedLinks = new();
    if (document != null)
    {
        HtmlNodeCollection links = document.DocumentNode.SelectNodes("//a[@href]");
        if(links != null)
        {
            foreach (var link in links)
            {
                Regex hrefreg = new Regex(HREF_PATTERN);
                getedLinks.AddRange(hrefreg.Matches(link.OuterHtml)
                   .ToList()
                   .ConvertAll(x =>
                       x.Value.Remove(0, 6)
                       .Remove(x.Length - 7)));//удаляю из полученной строки все до href=" включительно и в конце /"
            }
        }
    }

    return getedLinks.Where(x => x.StartsWith(DOMAIN) ||
        x.StartsWith("../") ||
        x.StartsWith("/") ||
        x.EndsWith(".html") && x.Count(x => x == '.') == 1 ||
        x.EndsWith("/") && !x.Contains("."))
        .ToList();
}

static Tuple<string, Link> GetHtmlDoc(string htmlForCheck)
{
    HtmlWeb hw = new HtmlWeb();
    try
    {
        HtmlDocument doc = hw.Load(htmlForCheck);
        return new Tuple<string, Link>(htmlForCheck, new(doc, hw.StatusCode));
    }
    catch (Exception e)
    {
        Console.WriteLine(htmlForCheck);
        Console.WriteLine(e.Message);
        return new Tuple<string, Link>(htmlForCheck, new(null!, hw.StatusCode));
    }

}

void FindAllInternalLinks(List<string> links, Dictionary<string, Link> visitedLinks)
{
    List<Action> actions = new();
    foreach (var link in links)
    {
        Tuple<string, Link> l = GetHtmlDoc(link.StartsWith(DOMAIN) ? DOMAIN : DOMAIN + link);
        if (l == null || visitedLinks.ContainsKey(l.Item1))
        {
            continue;
        }

        visitedLinks.Add(l.Item1, l.Item2);
        Console.WriteLine(l.Item1 + " " + (int)l.Item2.Status);

        if (l.Item2.Status == HttpStatusCode.OK)
        {
            actions.Add(() => FindAllInternalLinks(GetInternalLinks(l.Item2.Doc), visitedLinks));
        }
    }
    Parallel.Invoke(actions.ToArray());
}

static void PrintLinks(Dictionary<string, Link> visitedLinks)
{
    StreamWriter writerValid = new("../../../outputValid.txt", false);
    StreamWriter writerInValid = new("../../../outputInValid.txt", false);
    int valid = 0;
    int inValid = 0;
    foreach (var link in visitedLinks)
    {
        if(link.Value.Status == HttpStatusCode.OK)
        {
            valid++;
            writerValid.WriteLine(link.Key + " " + link.Value.Status + ":" + (int)link.Value.Status);
        }
        else
        {
            inValid++;
            writerInValid.WriteLine(link.Key + " " + link.Value.Status + ":" + (int)link.Value.Status);
        }
    }
    writerInValid.WriteLine("Count: " + inValid + "\nData: " + DateTime.Now.ToString());
    writerValid.WriteLine("Count: " + valid + "\nData: " + DateTime.Now.ToString());

    writerInValid.Close();
    writerValid.Close();
}

record Link(HtmlDocument Doc, HttpStatusCode Status);