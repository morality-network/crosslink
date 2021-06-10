using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossLinkX.Models
{
    public class Options
    {
        [JsonProperty("jsonLogs")]
        public bool JsonLogs { get; set; }

        [JsonProperty("logsFolder")]
        public string LogsFolder { get; set; }

        [JsonProperty("verbose")]
        public bool Verbose { get; set; }
    }
}
