using System.Xml.Serialization;

namespace Borkfolio.Application.Models.BoardGameGeek
{
    [XmlRoot(ElementName = "items")]
    public class BggSearchResultSet
    {
        [XmlAttribute(AttributeName = "total")]
        public int Count { get; set; }

        [XmlElement(ElementName = "item")]
        public List<BggSearchResultItem> Items { get; set; } = default!;
    }
}
