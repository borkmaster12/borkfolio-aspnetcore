using System.Xml.Serialization;

namespace Borkfolio.Application.Models.BoardGameGeek
{
    [XmlRoot(ElementName = "item")]
    public class BggCollectionItem
    {
        [XmlAttribute(AttributeName = "objectid")]
        public int Id { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; } = default!;

        [XmlElement(ElementName = "yearpublished")]
        public int Year { get; set; }
    }
}
