using CrossLinkX.Models;
using CrossLinkX.Services.Interfaces;
using Nethereum.StandardTokenEIP20;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace CrossLinkX.Services
{
    public class BinanceService : TokenServiceBase
    {
        public BinanceService(Web3 web3, TokenConnection tokenConnection) 
            : base(web3, tokenConnection)
        {
        }
    }
}
