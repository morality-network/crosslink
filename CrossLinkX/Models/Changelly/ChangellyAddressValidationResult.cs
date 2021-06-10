using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossLinkX.Models.Changelly
{
    public class ChangellyAddressValidationResult
    {
        [JsonProperty("result")]
        public bool Result { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
