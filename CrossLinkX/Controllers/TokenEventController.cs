using CrossLinkX.Models;
using CrossLinkX.Models.Enums;
using CrossLinkX.Services;
using CrossLinkX.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace CrossLinkX.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenEventController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public TokenEventController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("~/token/event")]
        public async Task<ActionResult> MovedTokenEvent([FromBody]CrosslinkEvent<CrosslinkRequest> eventModel)
        {
            // Check to see if we have the to chain populated - not point continuing if not
            if (eventModel?.Object?.ToChain == null || eventModel.Object.ToChain == BlockChain.UDF)
                return UnprocessableEntity("Chain MUST be populated");

            // Work out where the request is coming from
            var chainFrom = BlockChain.UDF;
            switch (eventModel.Object.ClassName)
            {
                case "EthereumMovedTokenRequests":
                    chainFrom = BlockChain.ETH;
                    break;
                case "BinanceMovedTokenRequests":
                    chainFrom = BlockChain.BNB;
                    break;
                default:
                    return UnprocessableEntity("Classname doesn't match any expected classname");
            }

            // Mint tokens
            var tx = await _tokenService.MintTokenAsync(eventModel.Object.Id, chainFrom, eventModel.Object.ToChain, 
                eventModel.Object.AddressTo, eventModel.Object.Amount);
            
            // Return status of ok
            return Ok();
        }
    }
}
