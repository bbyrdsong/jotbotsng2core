using System;
using Newtonsoft.Json;

namespace JotBotNg2Core.Lib
{
    public class IonLink
    {
        public string Href { get; set; }

        [JsonProperty(PropertyName = "rel", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Relations { get; set; }
        public DateTime Datetime { get; set; }
    }
}