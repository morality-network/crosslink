using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossLinkX.Models
{
    public class Log
    {
        [JsonProperty("options")]
        public Options Options { get; set; }

        [JsonProperty("appId")]
        public string AppId { get; set; }
    }
}
