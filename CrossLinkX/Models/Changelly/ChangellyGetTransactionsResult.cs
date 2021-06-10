using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossLinkX.Models.Changelly
{
    public class ChangellyGetTransactionsResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("trackUrl")]
        public string TrackUrl { get; set; }

        [JsonProperty("createdAt")]
        public int CreatedAt { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("moneyReceived")]
        public decimal? MoneyReceived { get; set; }

        [JsonProperty("moneySent")]
        public decimal? MoneySent { get; set; }

        [JsonProperty("rate")]
        public decimal? Rate { get; set; }

        [JsonProperty("payinConfirmations")]
        public string PayinConfirmations { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("currencyFrom")]
        public string CurrencyFrom { get; set; }

        [JsonProperty("currencyTo")]
        public string CurrencyTo { get; set; }

        [JsonProperty("payinAddress")]
        public string PayinAddress { get; set; }

        [JsonProperty("payinExtraId")]
        public string PayinExtraId { get; set; }

        [JsonProperty("payinExtraIdName")]
        public string PayinExtraIdName { get; set; }

        [JsonProperty("payinHash")]
        public string PayinHash { get; set; }

        [JsonProperty("payoutHashLink")]
        public string PayoutHashLink { get; set; }

        [JsonProperty("refundHashLink")]
        public string RefundHashLink { get; set; }

        [JsonProperty("amountExpectedFrom")]
        public string AmountExpectedFrom { get; set; }

        [JsonProperty("payoutAddress")]
        public string PayoutAddress { get; set; }

        [JsonProperty("payoutExtraId")]
        public string PayoutExtraId { get; set; }

        [JsonProperty("payoutExtraIdName")]
        public string PayoutExtraIdName { get; set; }

        [JsonProperty("payoutHash")]
        public string PayoutHash { get; set; }

        [JsonProperty("refundHash")]
        public string RefundHash { get; set; }

        [JsonProperty("amountFrom")]
        public string AmountFrom { get; set; }

        [JsonProperty("amountTo")]
        public string AmountTo { get; set; }

        [JsonProperty("amountExpectedTo")]
        public string AmountExpectedTo { get; set; }

        [JsonProperty("networkFee")]
        public decimal? NetworkFee { get; set; }

        [JsonProperty("changellyFee")]
        public decimal? ChangellyFee { get; set; }

        [JsonProperty("apiExtraFee")]
        public decimal? ApiExtraFee { get; set; }

        [JsonProperty("totalFee")]
        public decimal? TotalFee { get; set; }

        [JsonProperty("fiatProviderId")]
        public string FiatProviderId { get; set; }

        [JsonProperty("fiatProvider")]
        public string FiatProvider { get; set; }

        [JsonProperty("fiatProviderRedirect")]
        public string FiatProviderRedirect { get; set; }

        [JsonProperty("canPush")]
        public bool CanPush { get; set; }

        [JsonProperty("canRefund")]
        public bool CanRefund { get; set; }
    }
}
