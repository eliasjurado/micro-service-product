using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ProductAPI.DbContexts;
using ProductAPI.Models;
using ProductAPI.Models.Dtos;

namespace ProductAPI.Test.Fixtures
{
    public class ProductRepositoryFixture : IDisposable
    {
        public static string dbName = "dbProduct";
        public MemoryStream memoryStream => new MemoryStream();

        public IMapper mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<ProductDto, Product>();
            config.CreateMap<Product, ProductDto>();
        }).CreateMapper();

        public DbContextOptions<ApplicationDbContext> mockOptions => new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        public ApplicationDbContext dbContext => new ApplicationDbContext(mockOptions);

        public CancellationTokenSource cancellationTokenSource => new CancellationTokenSource(1000);
        public void Dispose()
        {
            memoryStream.Close();
        }
    }
}
