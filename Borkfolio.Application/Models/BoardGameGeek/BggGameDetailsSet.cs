using System.Xml.Serialization;

namespace Borkfolio.Application.Models.BoardGameGeek
{
    [XmlRoot(ElementName = "items")]
    public class BggGameDetailsSet
    {

        [XmlElement(ElementName = "item")]
        public List<BggGameDetailsItem> Items { get; set; } = default!;

    }
}
