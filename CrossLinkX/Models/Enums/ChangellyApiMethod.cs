using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossLinkX.Models.Enums
{
    /// <summary>
    /// Changelly request methods
    /// </summary>
    public enum ChangellyApiMethod
    {
        /// <summary>
        /// Get supported currency pairs
        /// </summary>
        getCurrencies = 0,

        /// <summary>
        /// Get minimum amount to exchange
        /// </summary>
        getMinAmount = 1,

        /// <summary>
        /// Get amount to exchange
        /// </summary>
        getExchangeAmount = 2,

        /// <summary>
        /// Validate address to send to
        /// </summary>
        validateAddress = 4,

        /// <summary>
        /// Create exchange transaction
        /// </summary>
        createTransaction = 8,

        /// <summary>
        /// Get all transactions
        /// </summary>
        getTransactions = 16,

        /// <summary>
        /// Get status of transaction
        /// </summary>
        getStatus = 32
    }
}
