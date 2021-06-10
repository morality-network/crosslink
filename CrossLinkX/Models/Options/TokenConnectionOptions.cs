using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossLinkX.Models
{
    public class TokenConnectionOptions
    {
        public bool UseTest { get; set; }
        public List<TokenConnection> TokenConnections { get; set; }
    }
}
