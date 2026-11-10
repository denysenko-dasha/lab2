using System.Xml;

namespace XmlProject;

public class SaxSearch : IXmlSearch
{
    public List<Club> Search(string xmlPath, SearchFilter filter)
    {
        var results = new List<Club>();

        using (XmlReader reader = XmlReader.Create(xmlPath))
        {

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Club")
                {
                    var club = new Club
                    {
                        Name = reader.GetAttribute("Name") ?? string.Empty,
                        Faculty = reader.GetAttribute("Faculty") ?? string.Empty,
                        Department = reader.GetAttribute("Department") ?? string.Empty,
                        Time = reader.GetAttribute("Time") ?? string.Empty,
                        HeadStudentName = reader.GetAttribute("HeadStudentName") ?? string.Empty,
                        HeadStudentCourse = reader.GetAttribute("HeadStudentCourse") ?? string.Empty,
                        TargetCourses = reader.GetAttribute("TargetCourses") ?? string.Empty,
                        Topic = reader.GetAttribute("Topic") ?? string.Empty,
                        CuratorName = reader.GetAttribute("CuratorName") ?? string.Empty,
                        IsTeacher = reader.GetAttribute("IsTeacher") ?? string.Empty
                    };

                    if (filter.MatchesFilter(club))
                    {
                        results.Add(club);
                    }
                }
            }
        }

        return results;
    }

    
}