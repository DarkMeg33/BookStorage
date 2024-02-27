using System.Xml.Linq;

namespace BookStorage.Extensions
{
    public static class XDocumentExtensions
    {
        public static XElement RemoveAllNamespaces(this XElement element)
        {
            return new XElement(element.Name.LocalName,
                (from n in element.Nodes()
                    select ((n is XElement) ? RemoveAllNamespaces(n as XElement) : n)),
                (element.HasAttributes) ? (from a in element.Attributes() select a) : null);
            //return new XElement(element.Name.LocalName,
            //    element.Nodes().Select(n => (n is XElement) ? RemoveAllNamespaces(n as XElement) : n)),
            //    element.HasAttributes ? element.Attributes() : null);
        }
    }
}