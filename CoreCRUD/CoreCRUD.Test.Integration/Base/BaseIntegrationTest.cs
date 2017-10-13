using CoreCRUD.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace CoreCRUD.Infrastructure.Test
{
    public class BaseIntegrationTest
    {
        public TestServer Server { get; set; }
        public HttpClient Client { get; set; }

        public BaseIntegrationTest()
        {
            Server = new TestServer(new WebHostBuilder()
                    .UseStartup<Startup>());
            Client = Server.CreateClient();
        }
    }
}
