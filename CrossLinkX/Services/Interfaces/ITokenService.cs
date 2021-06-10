using CrossLinkX.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace CrossLinkX.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> MintTokensAsync(BigInteger movedTokenId, BlockChain blockChain, string addressTo, BigInteger amount);
    }
}
