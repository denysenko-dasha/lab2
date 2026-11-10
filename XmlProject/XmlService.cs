using System.Xml.Linq;

namespace XmlProject;

public class XmlService
{
    private IXmlSearch search;
    public XmlService(IXmlSearch searchType)
    {
        search = searchType;
    }
    
    public void ChooseSearch(IXmlSearch searchType)
    {
        search = searchType;
    }

    public List<Club> Search(string xmlFile, SearchFilter filter)
    {
        return search.Search(xmlFile, filter);
    }

    public static Dictionary<string, List<string>> ExtractUniqueAttributes(string xmlFile)
    {
        XDocument doc = XDocument.Load(xmlFile);
        var clubs = doc.Descendants("Club");


        var allCourses = new List<string>();
        foreach (var club in clubs)
        {
            string coursesAttributes = (string?)club.Attribute("TargetCourses") ?? string.Empty;
            var parts = coursesAttributes.Split([',', '-', ';', ' '], StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var part in parts)
            {
                if (int.TryParse(part, out int courseNum))
                {
                    allCourses.Add(courseNum.ToString());
                }
            }
        }

        return new Dictionary<string, List<string>>
        {
            ["Faculties"] = clubs.Select(x => (string?)x.Attribute("Faculty") ?? string.Empty)
                                .Where(s => !string.IsNullOrEmpty(s))
                                .Distinct()
                                .ToList(),

            ["Topics"] = clubs.Select(x => (string?)x.Attribute("Topic") ?? string.Empty)
                            .Where(s => !string.IsNullOrEmpty(s))
                            .Distinct()
                            .ToList(),

            ["TargetCourses"] = allCourses.Distinct()
                                    .OrderBy(x => x)
                                    .ToList(),

            ["Curators"] = clubs.Select(x => (string?)x.Attribute("CuratorName") ?? string.Empty)
                                .Where(s => !string.IsNullOrEmpty(s))
                                .Distinct()
                                .ToList()
        };
    }
}