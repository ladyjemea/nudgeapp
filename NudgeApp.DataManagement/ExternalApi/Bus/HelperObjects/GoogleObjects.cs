using System;
using System.Collections.Generic;
using System.Text;

namespace NudgeApp.DataManagement.ExternalApi.Bus.HelperObjects
{
    internal class Distance
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    internal class Duration
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    internal class Element
    {
        public Distance distance { get; set; }
        public Duration duration { get; set; }
        public string status { get; set; }
    }

    internal class Row
    {
        public List<Element> elements { get; set; }
    }

    internal class RootObject
    {
        public List<string> destination_addresses { get; set; }
        public List<string> origin_addresses { get; set; }
        public List<Row> rows { get; set; }
        public string status { get; set; }
    }
}
