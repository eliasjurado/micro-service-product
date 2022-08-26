using ProductAPI.Test.Fixtures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        [Fact]
        public void ConfigureServices_IsLaunched()
        {
            var services = new ServiceCollection();
            services.AddSingleton(_fixture.mapper);
            var provider = services.BuildServiceProvider();

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();
            var result = new Startup(configuration);
            result.ConfigureServices(services);
            Assert.IsType<Startup>(result);
        }


    }
}
