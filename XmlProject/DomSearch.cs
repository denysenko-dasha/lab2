using System.Xml;

namespace XmlProject;

public class DomSearch : IXmlSearch
{
    public List<Club> Search(string xmlFile, SearchFilter filter)
    {
        var results = new List<Club>();
        XmlDocument doc = new XmlDocument();
        doc.Load(xmlFile);

        XmlNodeList? nodes = doc.SelectNodes("//Club");
        if (nodes == null)
        { 
            return results;
        }

        foreach (XmlNode node in nodes)
        {
            if (node.Attributes == null)
            {
                continue;
            } 

            var club = new Club
            {
                Name = node.Attributes["Name"]?.Value ?? string.Empty,
                Faculty = node.Attributes["Faculty"]?.Value ?? string.Empty,
                Department = node.Attributes["Department"]?.Value ?? string.Empty,
                Time = node.Attributes["Time"]?.Value ?? string.Empty,
                HeadStudentName = node.Attributes["HeadStudentName"]?.Value ?? string.Empty,
                HeadStudentCourse = node.Attributes["HeadStudentCourse"]?.Value ?? string.Empty,
                TargetCourses = node.Attributes["TargetCourses"]?.Value ?? string.Empty,
                Topic = node.Attributes["Topic"]?.Value ?? string.Empty,
                CuratorName = node.Attributes["CuratorName"]?.Value ?? string.Empty,
                IsTeacher = node.Attributes["IsTeacher"]?.Value ?? string.Empty
            };

            if (filter.MatchesFilter(club))
            {
                results.Add(club);
            }
        }

        return results;
    }

    
}