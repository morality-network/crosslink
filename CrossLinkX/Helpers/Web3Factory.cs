using CrossLinkX.Models;
using CrossLinkX.Models.Enums;
using Microsoft.Extensions.Options;
using Nethereum.KeyStore;
using Nethereum.KeyStore.Model;
using Nethereum.RPC.Accounts;
using Nethereum.Signer;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossLinkX.Helpers
{
    public class Web3Factory
    {
        private static ScryptParams _scryptParams = new ScryptParams { Dklen = 32, N = 262144, R = 1, P = 8 };

        /// <summary>
        /// Get an authenticated web3
        /// </summary>
        /// <returns>An authenticated web3</returns>
        public static Web3 CreateWeb3(TokenConnection tokenConnection, IOptions<KeyStoreOptions> keyStoreOptions)
        {
            // Get private key
            var pk = GetPrivateKeyFromKeystore($"{tokenConnection.Chain}-{tokenConnection.Network}", keyStoreOptions.Value.Password);

            // Get account 
            IAccount account = new Nethereum.Web3.Accounts.Account(pk.GetPrivateKey());

            // Create the web3 object
            return new Web3(account, tokenConnection.Url, null, null);
        }

        /// <summary>
        /// Store a key in a key store
        /// </summary>
        /// <param name="fileName">The file to save the keystore in</param>
        /// <param name="password">The password to encrypt with</param>
        /// <param name="privateKey">The private key to encrypt</param>
        public static void StoreKey(string fileName, string password, string privateKey)
        {
            var key = new EthECKey(privateKey);
            var service = new KeyStoreService();
            var keyStore = service.EncryptAndGenerateDefaultKeyStoreAsJson(
                        password, key.GetPrivateKeyAsBytes(), key.GetPublicAddress());
            var encodedKeystore = Encoding.UTF8.GetBytes(keyStore);

            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                stream.Write(encodedKeystore);
            }
        }

        /// <summary>
        /// Retrieves a private key from a key store
        /// </summary>
        /// <param name="fileName">The file where the key store is held</param>
        /// <param name="password">The password for the key store</param>
        /// <returns>A private key</returns>
        private static EthECKey GetPrivateKeyFromKeystore(string fileName, string password)
        {
            var keyStore = File.ReadAllText(fileName);

            var service = new KeyStoreService();
            var key = new EthECKey(
                    service.DecryptKeyStoreFromJson(password, keyStore),
                    true);

            return key;
        }

        /// <summary>
        /// Gets a token connection from base options
        /// </summary>
        /// <param name="options">The options to resolve token connection from</param>
        /// <param name="blockChain">The blockchain</param>
        /// <returns>A valid token connection</returns>
        public static TokenConnection GetTokenConnection(IOptions<TokenConnectionOptions> options, BlockChain blockChain)
        {
            return options.Value.TokenConnections.Where(x => x.Chain == blockChain)
                .Where(x => x.IsTest == options.Value.UseTest)
                .FirstOrDefault();
        }
    }
}
