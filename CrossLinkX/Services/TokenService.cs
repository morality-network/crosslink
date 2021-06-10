using CrossLinkX.Models;
using CrossLinkX.Models.Enums;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace CrossLinkX.Services
{
    public class TokenService
    {
        private readonly BinanceService _bnbService;
        private readonly EthereumService _ethService;
        private readonly ChangellyService _changellyService;

        public TokenService(BinanceService bncService, EthereumService ethService, ChangellyService changellyService)
        {
            _bnbService = bncService;
            _ethService = ethService;
            _changellyService = changellyService;
        }

        /// <summary>
        /// Mint tokens on a blockchain
        /// </summary>
        /// <param name="movedTokenId">The id of the moved token event (unique)</param>
        /// <param name="fromBlockChain">The blockchain that started the movement flow</param>
        /// <param name="toBlockChain">The blockcahin that the minting will take place on</param>
        /// <param name="addressTo">The address to recieve the minted tokens</param>
        /// <param name="amount">The amount to mint</param>
        /// <returns>An async task</returns>
        public async Task<string> MintTokenAsync(BigInteger movedTokenId, BlockChain fromBlockChain, BlockChain toBlockChain, string addressTo, BigInteger amount)
        {
            switch (toBlockChain)
            {
                case BlockChain.BNB:
                    return await _bnbService.MintTokensAsync(movedTokenId, fromBlockChain, addressTo, amount);
                case BlockChain.ETH:
                    return await _ethService.MintTokensAsync(movedTokenId, fromBlockChain, addressTo, amount);
                // The default value is undefined
                case BlockChain.UDF:
                default:
                    return null;
            }
        }

        /// <summary>
        /// Try to move the fees from one account to another
        /// </summary>
        /// <param name="fromChain"></param>
        /// <param name="toChain"></param>
        /// <returns></returns>
        public async Task<string> ExchangeFeesAsync(BlockChain fromChain, BlockChain toChain)
        {
            var baseCurrencyBalanceOfContract = 0.0m;
            var recieversOwnerAddress = string.Empty;

            // Get the balance of contract and the owner to 
            switch (fromChain)
            {
                case BlockChain.BNB:
                    baseCurrencyBalanceOfContract = await _bnbService.GetBaseCurrencyBalanceAsync(_bnbService.GetTokenAddress());
                    recieversOwnerAddress = _ethService.GetOwnersAddress();
                    break;
                case BlockChain.ETH:
                    baseCurrencyBalanceOfContract = await _ethService.GetBaseCurrencyBalanceAsync(_ethService.GetTokenAddress());
                    recieversOwnerAddress = _bnbService.GetOwnersAddress();
                    break;
                // The default value is undefined
                case BlockChain.UDF:
                default:
                    return null;
            }

            // Check if greater than the minimum exchange value
            var minimumExchangeValue = await _changellyService.GetMinAmount(new CurrencyPair() { From = fromChain.ToString(), To = toChain.ToString() });
            if (minimumExchangeValue?.Result == null || minimumExchangeValue.Result.Value > baseCurrencyBalanceOfContract)
                return null;

            // If we are here then we can exchange
            var transactionResult = await _changellyService.CreateTransaction(fromChain, toChain, recieversOwnerAddress, baseCurrencyBalanceOfContract);
            if (string.IsNullOrEmpty(transactionResult?.Result?.PayinAddress))
                return null;

            // Make the exchange
            switch (fromChain)
            {
                case BlockChain.BNB:
                    return await _bnbService.WithdrawFundsAsync("", transactionResult.Result.PayinAddress, Web3.Convert.ToWei(baseCurrencyBalanceOfContract)); //TODO:
                case BlockChain.ETH:
                    return await _ethService.WithdrawFundsAsync("", transactionResult.Result.PayinAddress, Web3.Convert.ToWei(baseCurrencyBalanceOfContract)); //TODO:
                // The default value is undefined
                case BlockChain.UDF:
                default:
                    return null;
            }
        }
    }
}
