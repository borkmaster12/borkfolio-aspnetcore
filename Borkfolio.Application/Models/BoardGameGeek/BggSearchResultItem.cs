using System.Xml.Serialization;

namespace Borkfolio.Application.Models.BoardGameGeek
{
    [XmlRoot(ElementName = "item")]
    public class BggSearchResultItem
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }

        public string Name
        {
            get { return _name.Value; }
        }

        public int Year
        {
            get { return Convert.ToInt32(_year?.Value); }
        }

        [XmlElement(ElementName = "name")]
        public NameElement _name { private get; init; } = default!;

        [XmlElement(ElementName = "yearpublished")]
        public YearElement _year { private get; init; } = default!;

        [XmlRoot(ElementName = "name")]
        public class NameElement
        {
            [XmlAttribute(AttributeName = "value")]
            public string Value { get; set; } = default!;
        }

        [XmlRoot(ElementName = "yearpublished")]
        public class YearElement
        {
            [XmlAttribute(AttributeName = "value")]
            public string Value { get; set; } = default!;
        }
    }
}
