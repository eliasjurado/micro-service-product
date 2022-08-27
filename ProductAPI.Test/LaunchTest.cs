using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ProductAPI.Test.Fixtures;
using Xunit;

namespace ProductAPI.Test
{
    public class LaunchTest : IClassFixture<ProductRepositoryFixture>
    {
        public readonly ProductRepositoryFixture _fixture;

        public LaunchTest(ProductRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Main_IsLaunched()
        {
            string[] args = { };
            var result = Program.CreateHostBuilder(args);
            Assert.IsType<HostBuilder>(result);
        }

        [Fact]
        public void Startup_IsLaunched()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();
            var result = new Startup(configuration);
            Assert.IsType<Startup>(result);
        }
    }
}
