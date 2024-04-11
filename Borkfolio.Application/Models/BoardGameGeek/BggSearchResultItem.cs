using System.Xml.Serialization;

namespace Borkfolio.Application.Models.BoardGameGeek
{
    [XmlRoot(ElementName = "item")]
    public class BggSearchResultItem
    {
        [XmlElement(ElementName = "id")]
        public int Id { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; } = default!;

        [XmlElement(ElementName = "year")]
        public int? Year { get; set; }

        [XmlElement(ElementName = "minage")]
        public int? MinimumAge { get; set; }
    }
}
