using Newtonsoft.Json;

namespace CrossLinkX.Models.Changelly
{
    public class ChangellyResult<T>
    {
        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("result")]
        public T Result { get; set; }
    }
}
