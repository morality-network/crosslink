using CrossLinkX.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace CrossLinkX.Models
{
    public class CrosslinkRequest
    {
        [JsonProperty("block_timestamp")]
        public BlockTimestamp BlockTimestamp { get; set; }

        [JsonProperty("transaction_hash")]
        public string TransactionHash { get; set; }

        [JsonProperty("log_index")]
        public int LogIndex { get; set; }

        [JsonProperty("uid")]
        public BigInteger Id { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("addressTo")]
        public string AddressTo { get; set; }

        [JsonProperty("fee")]
        public BigInteger Fee { get; set; }

        [JsonProperty("amount")]
        public BigInteger Amount { get; set; }

        [JsonProperty("toChain")]
        public BlockChain ToChain { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("block_hash")]
        public string BlockHash { get; set; }

        [JsonProperty("block_number")]
        public int BlockNumber { get; set; }

        [JsonProperty("transaction_index")]
        public int TransactionIndex { get; set; }

        [JsonProperty("className")]
        public string ClassName { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
