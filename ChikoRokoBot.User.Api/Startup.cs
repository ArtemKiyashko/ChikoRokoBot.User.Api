using System;
using Azure.Identity;
using ChikoRokoBot.User.Api.Options;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(ChikoRokoBot.User.Api.Startup))]
namespace ChikoRokoBot.User.Api
{
	public class Startup : FunctionsStartup
    {
        private IConfigurationRoot _functionConfig;
        private readonly UserApiOptions _apiOptions = new();

        public override void Configure(IFunctionsHostBuilder builder)
        {
            _functionConfig = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddAutoMapper(typeof(Startup));

            builder.Services.Configure<UserApiOptions>(_functionConfig.GetSection(nameof(UserApiOptions)));
            _functionConfig.GetSection(nameof(UserApiOptions)).Bind(_apiOptions);

            builder.Services.AddAzureClients(clientBuilder => {
                clientBuilder.UseCredential(new DefaultAzureCredential());

                if (Uri.TryCreate(_apiOptions.TableServiceConnection, UriKind.Absolute, out var tableServiceUri))
                    clientBuilder.AddTableServiceClient(tableServiceUri);
                else
                    clientBuilder.AddTableServiceClient(_apiOptions.TableServiceConnection);
            });
        }
    }
}

