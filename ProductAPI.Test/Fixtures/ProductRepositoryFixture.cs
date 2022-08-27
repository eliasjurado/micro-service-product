using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using ProductAPI.DbContexts;
using ProductAPI.Models;
using ProductAPI.Models.Dtos;

namespace ProductAPI.Test.Fixtures
{
    public class ProductRepositoryFixture : IDisposable
    {
        public string dbName = "dbProduct";

        public IMapper mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<ProductDto, Product>();
            config.CreateMap<Product, ProductDto>();
            config.CreateMap<ProcessProductDto, ProcessProduct>();
            config.CreateMap<ProcessProduct, ProcessProductDto>();
        }).CreateMapper();

        public IMapper badMapper = new MapperConfiguration(config =>
        {
        }).CreateMapper();

        public DbContextOptions<ApplicationDbContext> failOptions => new DbContextOptionsBuilder<ApplicationDbContext>().Options;

        public DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var mockOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .UseInternalServiceProvider(serviceProvider)
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .EnableSensitiveDataLogging()
            .Options;

            return mockOptions;
        }

        public void Dispose()
        {
        }
    }
}
