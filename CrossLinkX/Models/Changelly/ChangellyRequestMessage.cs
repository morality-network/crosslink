using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossLinkX.Models.Changelly
{
    public class ChangellyRequestMessage
    {
        [JsonProperty("jsonrpc")]
        public string JsonRpc { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public dynamic Params { get; set; }
    }
}
