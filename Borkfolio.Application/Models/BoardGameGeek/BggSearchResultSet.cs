using System.Xml.Serialization;

namespace Borkfolio.Application.Models.BoardGameGeek
{
    [XmlRoot(ElementName = "items")]
    public class BggSearchResultSet
    {
        [XmlElement(ElementName = "total")]
        public int? Count { get; set; }

        [XmlElement(ElementName = "item")]
        List<BggSearchResultItem>? Items { get; set; }
    }
}
