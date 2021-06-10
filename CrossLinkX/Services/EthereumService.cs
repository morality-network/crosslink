using CrossLinkX.Models;
using CrossLinkX.Services.Interfaces;
using Microsoft.Extensions.Options;
using Nethereum.Hex.HexTypes;
using Nethereum.KeyStore;
using Nethereum.StandardTokenEIP20;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace CrossLinkX.Services
{
    public class EthereumService : TokenServiceBase
    {
        public EthereumService(Web3 web3, TokenConnection tokenConnection) 
            : base(web3, tokenConnection)
        {
        }
    }
}
