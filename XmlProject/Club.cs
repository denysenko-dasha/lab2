namespace XmlProject;

public class Club
{
    public string Name { get; set; } = string.Empty;

    public string Faculty { get; set; } = string.Empty;

    public string Department { get; set; } = string.Empty;

    public string Time { get; set; } = string.Empty; 
    
    public string HeadStudentName { get; set; } = string.Empty; 

    public string HeadStudentCourse { get; set; } = string.Empty; 

    public string TargetCourses { get; set; } = string.Empty; 

    public string Topic { get; set; } = string.Empty; 

    public string CuratorName { get; set; } = string.Empty; 
    
    public string IsTeacher { get; set; } = string.Empty;

    public List<int> TargetCoursesList
    {
        get
        {
            var courses = new List<int>();
            if (string.IsNullOrWhiteSpace(TargetCourses)) return courses;
            
            var parts = TargetCourses.Split([',', '-', ';', ' '], StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in parts)
            {
                if (int.TryParse(part, out int courseNum))
                {
                    courses.Add(courseNum);
                }
            }

            return courses;
        }
    } 
}