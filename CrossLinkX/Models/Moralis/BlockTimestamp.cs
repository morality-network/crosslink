using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossLinkX.Models
{
    public class BlockTimestamp
    {
        [JsonProperty("__type")]
        public string Type { get; set; }

        [JsonProperty("iso")]
        public DateTime Iso { get; set; }
    }
}
