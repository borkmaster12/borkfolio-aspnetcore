using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Borkfolio.Application.Models.BoardGameGeek
{
    public class BggGameDetailsItem
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

        public int MinimumAge
        {
            get { return Convert.ToInt32(_minimumAge?.Value); }
        }

        [JsonIgnore]
        [XmlElement(ElementName = "name")]
        public NameElement _name { private get; init; } = default!;

        [JsonIgnore]
        [XmlElement(ElementName = "yearpublished")]
        public YearElement _year { private get; init; } = default!;

        [JsonIgnore]
        [XmlElement(ElementName = "minage")]
        public YearElement _minimumAge { private get; init; } = default!;

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

        [XmlRoot(ElementName = "minage")]
        public class MinimumAgeElement
        {
            [XmlAttribute(AttributeName = "value")]
            public string Value { get; set; } = default!;
        }
    }
}