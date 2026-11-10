using System.Xml.Xsl;

namespace XmlProject;

public static class XsltTransformer
{
    public static void TransformToHtml(string xmlPath, string xslPath, string htmlPath)
    {
        XslCompiledTransform transform = new XslCompiledTransform();
        transform.Load(xslPath);
        transform.Transform(xmlPath, htmlPath);
    }
}