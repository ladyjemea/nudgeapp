namespace NudgeApp.DataManagement.ExternalApi.Bus.HelperObjects
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "i")]
    public class I
    {
        [XmlAttribute(AttributeName = "pt")]
        public string Pt { get; set; }
        [XmlAttribute(AttributeName = "n")]
        public string N { get; set; }
        [XmlAttribute(AttributeName = "n2")]
        public string N2 { get; set; }
        [XmlAttribute(AttributeName = "nd")]
        public string Nd { get; set; }
        [XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
        [XmlAttribute(AttributeName = "hplnr")]
        public string Hplnr { get; set; }
        [XmlAttribute(AttributeName = "stopnr")]
        public string Stopnr { get; set; }
        [XmlAttribute(AttributeName = "v2")]
        public string V2 { get; set; }
        [XmlAttribute(AttributeName = "stopnr2")]
        public string Stopnr2 { get; set; }
        [XmlAttribute(AttributeName = "t")]
        public string T { get; set; }
        [XmlAttribute(AttributeName = "i")]
        public string _i { get; set; }
        [XmlAttribute(AttributeName = "st")]
        public string St { get; set; }
        [XmlAttribute(AttributeName = "tt")]
        public string Tt { get; set; }
        [XmlAttribute(AttributeName = "tn")]
        public string Tn { get; set; }
        [XmlAttribute(AttributeName = "ti")]
        public string Ti { get; set; }
        [XmlAttribute(AttributeName = "tp")]
        public string Tp { get; set; }
        [XmlAttribute(AttributeName = "td")]
        public string Td { get; set; }
        [XmlAttribute(AttributeName = "x")]
        public string X { get; set; }
        [XmlAttribute(AttributeName = "y")]
        public string Y { get; set; }
        [XmlAttribute(AttributeName = "l")]
        public string L { get; set; }
        [XmlAttribute(AttributeName = "ns")]
        public string Ns { get; set; }
        [XmlAttribute(AttributeName = "c")]
        public string C { get; set; }
        [XmlAttribute(AttributeName = "p")]
        public string P { get; set; }
        [XmlAttribute(AttributeName = "tid")]
        public string Tid { get; set; }
        [XmlAttribute(AttributeName = "tno")]
        public string Tno { get; set; }
        [XmlAttribute(AttributeName = "d")]
        public string D { get; set; }
        [XmlAttribute(AttributeName = "a")]
        public string A { get; set; }
        [XmlAttribute(AttributeName = "fnt")]
        public string Fnt { get; set; }
        [XmlAttribute(AttributeName = "tnt")]
        public string Tnt { get; set; }
        [XmlAttribute(AttributeName = "wn")]
        public string Wn { get; set; }
        [XmlAttribute(AttributeName = "updateid")]
        public string Updateid { get; set; }
        [XmlAttribute(AttributeName = "geometryid")]
        public string Geometryid { get; set; }
    }

    [XmlRoot(ElementName = "trip")]
    public class Trip
    {
        [XmlElement(ElementName = "i")]
        public List<I> I { get; set; }
        [XmlAttribute(AttributeName = "duration")]
        public string Duration { get; set; }
        [XmlAttribute(AttributeName = "start")]
        public string Start { get; set; }
        [XmlAttribute(AttributeName = "stop")]
        public string Stop { get; set; }
        [XmlAttribute(AttributeName = "changecount")]
        public string Changecount { get; set; }
        [XmlAttribute(AttributeName = "priceid")]
        public string Priceid { get; set; }

        public Trip()
        {
            this.I = new List<I>();
        }
    }

    [XmlRoot(ElementName = "trips")]
    public class Trips
    {
        [XmlElement(ElementName = "trip")]
        public List<Trip> Trip { get; set; }
        [XmlAttribute(AttributeName = "p")]
        public string P { get; set; }
        [XmlAttribute(AttributeName = "t")]
        public string T { get; set; }

        public Trips()
        {
            this.Trip = new List<Trip>();
        }
    }

    [XmlRoot(ElementName = "prices")]
    public class Prices
    {
        [XmlElement(ElementName = "trips")]
        public List<Trips> Trips { get; set; }

        public Prices()
        {
            this.Trips = new List<Trips>();
        }
    }

    [XmlRoot(ElementName = "result")]
    public class TripObject
    {
        [XmlElement(ElementName = "trips")]
        public Trips Trips { get; set; }
        [XmlElement(ElementName = "prices")]
        public Prices Prices { get; set; }

        public TripObject()
        {
            this.Trips = new Trips();
            this.Prices = new Prices();
        }

        [XmlAttribute(AttributeName = "from")]
        public string From { get; set; }
        [XmlAttribute(AttributeName = "to")]
        public string To { get; set; }
        [XmlAttribute(AttributeName = "travelnote")]
        public string Travelnote { get; set; }
        [XmlAttribute(AttributeName = "pricenote")]
        public string Pricenote { get; set; }
    }
}