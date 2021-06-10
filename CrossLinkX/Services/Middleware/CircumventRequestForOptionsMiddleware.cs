using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CrossLinkX.Services.Middleware
{
    public class CircumventRequestForOptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CircumventRequestForOptionsMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Constructor for the middleware
        /// </summary>
        /// <param name="next">The next middleware to be executed</param>
        /// <param name="logger">The class logger</param>
        /// <param name="env">Current environment this is running under</param>
        public CircumventRequestForOptionsMiddleware(RequestDelegate next, ILogger<CircumventRequestForOptionsMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
            _env = env;
        }

        /// <summary>
        /// Invoked via reflection on request passthrough
        /// </summary>
        /// <param name="context">The request context</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            // Return ok if its an options call (don't want to forward to post method)
            if (context.Request.Method == HttpMethods.Options)
            {
                await Ok(context);
                return;
            }

            await _next(context);  // Call next middleware if we can
        }

        /// <summary>
        /// Replies with a 200 response
        /// </summary>
        /// <param name="context">The context to make the response to</param>
        /// <returns>A completed task</returns>
        private async Task Ok(HttpContext context)
        {
            context.Response.StatusCode = ((int)HttpStatusCode.OK);
            await context.Response.WriteAsync(string.Empty);
            return;
        }
    }
}
