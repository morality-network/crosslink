using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrossLinkX.Models
{
    public class CrosslinkEvent<T>
    {
        [JsonProperty("triggerName")]
        public string TriggerName { get; set; }

        [JsonProperty("object")]
        [Required]
        public T Object { get; set; }

        [JsonProperty("master")]
        public bool Master { get; set; }

        [JsonProperty("log")]
        public Log Log { get; set; }

        [JsonProperty("installationId")]
        public string InstallationId { get; set; }
    }
}
