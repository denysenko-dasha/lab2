using System.Xml.Linq;

namespace XmlProject;

public class LinqToXmlSearch : IXmlSearch
{
    public List<Club> Search(string xmlFile, SearchFilter filter)
    {
        XDocument doc = XDocument.Load(xmlFile);

        var query = from clubNode in doc.Descendants("Club")
                    let club = new Club
                    {
                        Name = (string?)clubNode.Attribute("Name") ?? string.Empty,
                        Faculty = (string?)clubNode.Attribute("Faculty") ?? string.Empty,
                        Department = (string?)clubNode.Attribute("Department") ?? string.Empty,
                        Time = (string?)clubNode.Attribute("Time") ?? string.Empty,
                        HeadStudentName = (string?)clubNode.Attribute("HeadStudentName") ?? string.Empty,
                        HeadStudentCourse = (string?)clubNode.Attribute("HeadStudentCourse") ?? string.Empty,
                        TargetCourses = (string?)clubNode.Attribute("TargetCourses") ?? string.Empty,
                        Topic = (string?)clubNode.Attribute("Topic") ?? string.Empty,
                        CuratorName = (string?)clubNode.Attribute("CuratorName") ?? string.Empty,
                        IsTeacher = (string?)clubNode.Attribute("IsTeacher") ?? string.Empty
                    }
                    where filter.MatchesFilter(club)
                    select club;

        return query.ToList();
    }
}