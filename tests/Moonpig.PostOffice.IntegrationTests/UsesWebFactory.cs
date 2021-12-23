using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Moonpig.PostOffice.Api;
using Xunit.Abstractions;

namespace Moonpig.PostOffice.IntegrationTests
{
    public class UsesWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly ITestOutputHelper _output;

        public UsesWebApplicationFactory(ITestOutputHelper outputHelper) => _output = outputHelper;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .UseEnvironment("Development")
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddXUnit(_output);
                });

            base.ConfigureWebHost(builder);
        }
    }
}