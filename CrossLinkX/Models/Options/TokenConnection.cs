using CrossLinkX.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossLinkX.Models
{
    public class TokenConnection
    {
        public BlockChain Chain { get; set; }
        public Network Network { get; set; }
        public string Url { get; set; }
        public bool IsTest { get; set; }
        public string ContractAddress { get; set; }
        public string OwnerAddress { get; set; }
        public string Abi { get; set; }
    }
}
