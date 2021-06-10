using CrossLinkX.Helpers;
using CrossLinkX.Models;
using CrossLinkX.Models.Enums;
using CrossLinkX.Services;
using CrossLinkX.Services.Hosted;
using CrossLinkX.Services.Interfaces;
using CrossLinkX.Services.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nethereum.KeyStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CrossLinkX
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(opts => opts.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter()))
                .AddJsonOptions(opt => { opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

            // Setup the options
            services.Configure<KeyStoreOptions>(options => Configuration.GetSection("KeyStoreOptions").Bind(options));
            services.Configure<TokenConnectionOptions>(options => Configuration.GetSection("TokenConnectionOptions").Bind(options));
            services.Configure<ChangellyOptions>(options => Configuration.GetSection("ChangellyOptions").Bind(options));
            services.Configure<FeeExchangeServiceOptions>(options => Configuration.GetSection("FeeExchangeServiceOptions").Bind(options));

            // Setup http clients
            services.AddHttpClient("ChangellyService", (HttpClient client) => { client.BaseAddress = new Uri(Configuration.GetValue<string>("ChangellyOptions:Uri")); });

            // Setup the DI
            services.AddTransient<ChangellyService>(s => new ChangellyService(s.GetService<IHttpClientFactory>().CreateClient("ChangellyService"), s.GetService<IOptions<ChangellyOptions>>()));
            services.AddTransient<EthereumService>(x => new EthereumService(
                Web3Factory.CreateWeb3(
                    Web3Factory.GetTokenConnection(x.GetRequiredService<IOptions<TokenConnectionOptions>>(), BlockChain.ETH),
                    x.GetRequiredService<IOptions<KeyStoreOptions>>()
                ),
                Web3Factory.GetTokenConnection(x.GetRequiredService<IOptions<TokenConnectionOptions>>(), BlockChain.ETH)
            ));
            services.AddTransient<BinanceService>(x => new BinanceService(
                Web3Factory.CreateWeb3(
                    Web3Factory.GetTokenConnection(x.GetRequiredService<IOptions<TokenConnectionOptions>>(), BlockChain.BNB),
                    x.GetRequiredService<IOptions<KeyStoreOptions>>()
                ),
                Web3Factory.GetTokenConnection(x.GetRequiredService<IOptions<TokenConnectionOptions>>(), BlockChain.BNB)
            ));
            services.AddTransient<TokenService>();

            // Add the fee monitor
            services.AddHostedService<FeeExchangeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<CircumventRequestForOptionsMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
