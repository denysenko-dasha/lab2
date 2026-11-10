namespace XmlProject;

public interface IXmlSearch
{
    List<Club> Search(string xmlPath, SearchFilter filter);
}