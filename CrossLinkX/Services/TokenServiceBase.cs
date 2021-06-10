using CrossLinkX.Models;
using CrossLinkX.Models.Enums;
using CrossLinkX.Services.Interfaces;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.StandardTokenEIP20;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace CrossLinkX.Services
{
    public abstract class TokenServiceBase : StandardTokenService, ITokenService
    {
        /// <summary>
        /// The block chain connection details
        /// </summary>
        private readonly TokenConnection _tokenConnection;

        /// <summary>
        /// Base for token services
        /// </summary>
        /// <param name="web3">A blockchain connection</param>
        /// <param name="tokenConnection"></param>
        public TokenServiceBase(Web3 web3, TokenConnection tokenConnection)
            : base(web3, tokenConnection.ContractAddress)
        {
            _tokenConnection = tokenConnection;
        }

        /// <summary>
        /// Mint a token for a contract
        /// </summary>
        /// <param name="movedTokenId">The unique id of the movement</param>
        /// <param name="fromBlockChain">The blockchain that the redeption originated from</param>
        /// <param name="addressTo">The address the tokens will be minted to</param>
        /// <param name="amount">The amount of tokens to mint</param>
        /// <returns>The transaction hash</returns>
        public async virtual Task<string> MintTokensAsync(BigInteger movedTokenId, BlockChain fromBlockChain, string addressTo, BigInteger amount)
        {
            // Get the contract to mint token for
            var contract = BuildContract();

            // Get the mint function to request
            var mintToken = contract.GetFunction("redeemToken");

            // Estimate the gas cost
            var gas = await mintToken.EstimateGasAsync(_tokenConnection.OwnerAddress, null, null, movedTokenId, fromBlockChain.ToString(), addressTo, amount);

            // Mint and return tx hash
            return await mintToken.SendTransactionAsync(_tokenConnection.OwnerAddress, gas, (HexBigInteger)null, movedTokenId, fromBlockChain.ToString(), addressTo, amount);
        }

        /// <summary>
        /// Gets the balance of an address is the base currency of the blockchain the token is hosted on
        /// ie. Ethereum network = ETH and Binance network = BNB
        /// </summary>
        /// <param name="address">The address to get balance of</param>
        /// <returns>The balance of address</returns>
        public async virtual Task<decimal> GetBaseCurrencyBalanceAsync(string address)
        {
            // Get the current balance
            var currentBalance = await Web3.Eth.GetBalance.SendRequestAsync(address);

            // Convert from wei and return
            return Web3.Convert.FromWei(currentBalance.Value);
        }

        /// <summary>
        /// Recover base chain currency from a contract (ie. if on Ethereum chain then ETH and if on Binance chain BNB)
        /// </summary>
        /// <param name="addressTo">The address to recover funds to</param>
        /// <param name="amount">The amount of funds to recover</param>
        /// <returns>A transaction hash</returns>
        public async virtual Task<string> WithdrawFundsAsync(string addressFrom, string addressTo, BigInteger amount)
        {
            // Get the contract to withdraw from
            var contract = BuildContract();

            // Get the withdraw function to request
            var mintToken = contract.GetFunction("withdraw");

            // Estimate the gas cost
            var gas = await mintToken.EstimateGasAsync(addressFrom, null, null, amount, addressTo);

            // Withdraw and return tx hash
            return await mintToken.SendTransactionAsync(addressFrom, gas, (HexBigInteger)null, amount, addressTo);
        }

        /// <summary>
        /// Gets the services contract (token) address
        /// </summary>
        /// <returns>The service contract (token) address</returns>
        public virtual string GetTokenAddress()
        {
            return _tokenConnection.ContractAddress;
        }

        /// <summary>
        /// Gets the owners address for the contract
        /// </summary>
        /// <returns>The owners address</returns>
        public virtual string GetOwnersAddress()
        {
            return _tokenConnection.OwnerAddress;
        }

        #region Helpers

        /// <summary>
        /// Get a contract
        /// </summary>
        /// <returns>The contract this service is attributed to</returns>
        private Contract BuildContract()
        {
            return Web3.Eth.GetContract(_tokenConnection.Abi, _tokenConnection.ContractAddress);
        }

        #endregion
    }
}
