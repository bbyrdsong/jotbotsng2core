using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace JotBotNg2Core.Lib
{
    public abstract class IonResource
    {
        [NotMapped]
        [JsonProperty(Order = -2)]
        public IonLink Meta { get; set; }
    }
}