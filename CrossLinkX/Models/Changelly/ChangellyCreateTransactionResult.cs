using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossLinkX.Models.Changelly
{
    public class ChangellyCreateTransactionResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("apiExtraFee")]
        public decimal? ApiExtraFee { get; set; }

        [JsonProperty("changellyFee")]
        public decimal? ChangellyFee { get; set; }

        [JsonProperty("payinExtraId")]
        public string PayinExtraId { get; set; }

        [JsonProperty("amountExpectedFrom")]
        public decimal? AmountExpectedFrom { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("currencyFrom")]
        public string CurrencyFrom { get; set; }

        [JsonProperty("currencyTo")]
        public string CurrencyTo { get; set; }

        [JsonProperty("amountTo")]
        public decimal? AmountTo { get; set; }

        [JsonProperty("amountExpectedTo")]
        public decimal? AmountExpectedTo { get; set; }

        [JsonProperty("payinAddress")]
        public string PayinAddress { get; set; }

        [JsonProperty("PayoutAddress")]
        public string payoutAddress { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("redirect")]
        public string Redirect { get; set; }

        [JsonProperty("kycRequired")]
        public bool KycRequired { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("binaryPayload")]
        public string BinaryPayload { get; set; }
    }
}
