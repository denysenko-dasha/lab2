namespace XmlProject;

public class SearchFilter
{
    public string Faculty { get; set; } = string.Empty; 
     
    public string Topic { get; set; } = string.Empty;

    public List<int> TargetCourses { get; set; } = new List<int>();

    public string CuratorName { get; set; } = string.Empty;

    public bool MatchesFilter(Club club)
    {
        if (!string.IsNullOrEmpty(Faculty) && !club.Faculty.Equals(Faculty, StringComparison.OrdinalIgnoreCase))
            return false;
        if (!string.IsNullOrWhiteSpace(Topic) && !club.Topic.Contains(Topic, StringComparison.OrdinalIgnoreCase))
            return false;
        if (TargetCourses != null && TargetCourses.Count > 0)
        {
            if (!TargetCourses.Any(club.TargetCoursesList.Contains))
                return false;
        }
        if (!string.IsNullOrWhiteSpace(CuratorName) && !club.CuratorName.Contains(CuratorName, StringComparison.OrdinalIgnoreCase))
            return false;

        return true;
    } 
}