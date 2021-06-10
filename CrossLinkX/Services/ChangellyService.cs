using CrossLinkX.Models;
using CrossLinkX.Models.Changelly;
using CrossLinkX.Models.Enums;
using CrossLinkX.Utils;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CrossLinkX.Services
{
    public class ChangellyService : BaseProxyService
    {
        private readonly Encoding _utf8 = Encoding.UTF8;
        private readonly ChangellyOptions _changellyOptions;

        public ChangellyService(HttpClient client, IOptions<ChangellyOptions> changellyOptions)
            : base(client)
        {
            _changellyOptions = changellyOptions.Value;
        }

        /// <summary>
        /// Returns a list of enabled currencies as a flat array.
        /// </summary>
        /// <returns></returns>
        public async Task<ChangellyResult<List<string>>> GetCurrencies()
        {
            var message = BuildMessage(1, ChangellyApiMethod.getCurrencies, new string[0]);
            var serializedMessage = JsonConvert.SerializeObject(message);
            var sign = SignMessage(serializedMessage);
            var result = await PostStringAsync<ChangellyResult<List<string>>>(serializedMessage, GetHeaders(sign));
            return result;
        }

        /// <summary>
        /// Returns a minimum allowed payin amount required for a currency pair. Amounts less than a minimal will most likely fail the transaction.
        /// </summary>
        /// <param name="pair"></param>
        /// <returns>Minimum amount to send to trade currency pair</returns>
        public async Task<ChangellyResult<decimal?>> GetMinAmount(CurrencyPair pair)
        {
            var message = BuildMessage(1, ChangellyApiMethod.getMinAmount, new { from = pair.From, to = pair.To }); // From-To
            var serializedMessage = JsonConvert.SerializeObject(message);
            var sign = SignMessage(serializedMessage);
            var result = await PostStringAsync<ChangellyResult<decimal?>>(serializedMessage, GetHeaders(sign));
            return result;
        }

        /// <summary>
        /// Returns estimated exchange value with your API partner fee included.
        /// </summary>
        /// <param name="pair">To, From and Amount</param>
        /// <returns>Returns how much would be returned given a transaction was made with the same params</returns>
        public async Task<ChangellyResult<decimal?>> GetExchangeAmount(CurrencyPair pair)
        {
            var message = BuildMessage(1, ChangellyApiMethod.getExchangeAmount, new { from = pair.From, to = pair.To, amount = pair.Amount });
            var serializedMessage = JsonConvert.SerializeObject(message);
            var sign = SignMessage(serializedMessage);
            var result = await PostStringAsync<ChangellyResult<decimal?>>(serializedMessage, GetHeaders(sign));
            return result;
        }

        /// <summary>
        /// Gets transaction by id
        /// </summary>
        /// <param name="transactionId">The transaction id to filter by (default none)</param>
        /// <returns>Transactions</returns>
        public async Task<ChangellyResult<List<ChangellyGetTransactionsResult>>> GetTransactions(string transactionId = null)
        {
            var args = new { id = transactionId };
            var message = BuildMessage(1, ChangellyApiMethod.getTransactions, !string.IsNullOrEmpty(transactionId) ? args : new object());
            var serializedMessage = JsonConvert.SerializeObject(message);
            var sign = SignMessage(serializedMessage);
            var result = await PostStringAsync<ChangellyResult<List<ChangellyGetTransactionsResult>>>(serializedMessage, GetHeaders(sign));
            return result;
        }

        /// <summary>
        /// Gets transaction by id
        /// </summary>
        /// <param name="transactionId">The transaction id to filter by</param>
        /// <returns>Transactions</returns>
        public async Task<ChangellyResult<string>> GetStatus(string transactionId)
        {
            var message = BuildMessage(1, ChangellyApiMethod.getStatus, new { id = transactionId });
            var serializedMessage = JsonConvert.SerializeObject(message);
            var sign = SignMessage(serializedMessage);
            var result = await PostStringAsync<ChangellyResult<string>>(serializedMessage, GetHeaders(sign));
            return result;
        }

        /// <summary>
        /// Returns if a given address is valid or not for a given currency.
        /// </summary>
        /// <param name="currency">The currency of the address to validate</param>
        /// <param name="address">The address to validate</param>
        /// <returns>The validation status</returns>
        public async Task<ChangellyResult<ChangellyAddressValidationResult>> ValidateAddress(string currency, string address)
        {
            var message = BuildMessage(1, ChangellyApiMethod.validateAddress, new { address = address, currency = currency });
            var serializedMessage = JsonConvert.SerializeObject(message);
            var sign = SignMessage(serializedMessage);
            var result = await PostStringAsync<ChangellyResult<ChangellyAddressValidationResult>>(serializedMessage, GetHeaders(sign));
            return result;
        }

        /// <summary>
        /// Creates a new transaction, generates a pay-in address and returns Transaction object with an ID field to track a transaction status.
        /// </summary>
        /// <param name="pair">The pair to trade</param>
        /// <param name="payoutAddress">Address to send exchanged value to</param>
        /// <param name="amount">The amount to exchange</param>
        /// <returns></returns>
        public async Task<ChangellyResult<ChangellyCreateTransactionResult>> CreateTransaction(BlockChain from, BlockChain to, string payoutAddress, decimal amount)
        {
            var message = BuildMessage(1, ChangellyApiMethod.createTransaction, new { from = from.ToString().ToLower(), to = to.ToString().ToLower(), address = payoutAddress, amount = amount });
            var serializedMessage = JsonConvert.SerializeObject(message);
            var sign = SignMessage(serializedMessage);
            var result = await PostStringAsync<ChangellyResult<ChangellyCreateTransactionResult>>(serializedMessage, GetHeaders(sign));
            return result;
        }

        /// <summary>
        /// Sign the message to send (with secret)
        /// </summary>
        /// <param name="message">The raw message</param>
        /// <returns>Signed hexadecimal message</returns>
        private string SignMessage(string message)
        {
            HMACSHA512 hmac = new HMACSHA512(_utf8.GetBytes(_changellyOptions.Secret));
            byte[] hashmessage = hmac.ComputeHash(_utf8.GetBytes(message));

            return hashmessage.ToHexString();
        }

        /// <summary>
        /// Gets required headers for succesful call
        /// </summary>
        /// <param name="sign">The value to sign call with (add to header)</param>
        /// <returns>List of headers</returns>
        private List<KeyValuePair<string, string>> GetHeaders(string sign)
        {
            var headers = new List<KeyValuePair<string, string>>();

            headers.Add(new KeyValuePair<string, string>("api-key", _changellyOptions.ApiKey));
            headers.Add(new KeyValuePair<string, string>("sign", sign));
            
            return headers;
        }

        /// <summary>
        /// Creates request message 
        /// </summary>
        /// <param name="id">The id of the request</param>
        /// <param name="method">Method to call</param>
        /// <param name="parameters">The parameters that correspond to the method called</param>
        /// <returns>A complete request message</returns>
        private ChangellyRequestMessage BuildMessage(int id, ChangellyApiMethod method, dynamic parameters)
        {
            return new ChangellyRequestMessage()
            {
                Id = id,
                JsonRpc = "2.0",
                Method = method.ToString(),
                Params = parameters
            };
        }
    }
}
