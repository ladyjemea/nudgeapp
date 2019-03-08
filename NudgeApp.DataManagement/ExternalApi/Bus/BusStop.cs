namespace NudgeApp.DataManagement.ExternalApi.Bus.BusStop
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "zone")]
    public class Zone
    {
        [XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
        [XmlAttribute(AttributeName = "n")]
        public string N { get; set; }
    }

    [XmlRoot(ElementName = "zones")]
    public class Zones
    {
        [XmlElement(ElementName = "zone")]
        public List<Zone> Zone { get; set; }
    }

    [XmlRoot(ElementName = "i")]
    public class I
    {
        [XmlElement(ElementName = "zones")]
        public Zones Zones { get; set; }
        [XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
        [XmlAttribute(AttributeName = "hplnr")]
        public string Hplnr { get; set; }
        [XmlAttribute(AttributeName = "n")]
        public string N { get; set; }
        [XmlAttribute(AttributeName = "t")]
        public string T { get; set; }
        [XmlAttribute(AttributeName = "d")]
        public string D { get; set; }
        [XmlAttribute(AttributeName = "m")]
        public string M { get; set; }
        [XmlAttribute(AttributeName = "x")]
        public string X { get; set; }
        [XmlAttribute(AttributeName = "y")]
        public string Y { get; set; }
        [XmlAttribute(AttributeName = "stopnr")]
        public string Stopnr { get; set; }
        [XmlAttribute(AttributeName = "tn")]
        public string Tn { get; set; }
        [XmlAttribute(AttributeName = "st")]
        public string St { get; set; }
        [XmlAttribute(AttributeName = "l")]
        public string L { get; set; }
    }

    [XmlRoot(ElementName = "group")]
    public class Group
    {
        [XmlElement(ElementName = "i")]
        public List<I> I { get; set; }
        [XmlAttribute(AttributeName = "n")]
        public string N { get; set; }
        [XmlAttribute(AttributeName = "d")]
        public string D { get; set; }
        [XmlAttribute(AttributeName = "m")]
        public string M { get; set; }
        [XmlAttribute(AttributeName = "x")]
        public string X { get; set; }
        [XmlAttribute(AttributeName = "y")]
        public string Y { get; set; }
    }

    [XmlRoot(ElementName = "stages")]
    public class Stages
    {
        [XmlElement(ElementName = "group")]
        public List<Group> Group { get; set; }
    }


}
