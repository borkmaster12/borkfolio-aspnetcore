using System.Xml.Serialization;

namespace Borkfolio.Application.Models.BoardGameGeek
{
    [XmlRoot(ElementName = "items")]
    public class BggCollection
    {
        [XmlAttribute(AttributeName = "totalitems")]
        public int Count { get; set; }

        [XmlElement(ElementName = "item")]
        public List<BggCollectionItem> Items { get; set; } = default!;
    }
}
