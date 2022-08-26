using ProductAPI.Controllers;
using ProductAPI.DbContexts;
using ProductAPI.Models.Dto;
using ProductAPI.Models.Dtos;
using ProductAPI.Repository;
using ProductAPI.Test.DataAttributes;
using ProductAPI.Test.Fixtures;
using Xunit;

namespace ProductAPI.Test
{
    public class ProcessProductControllerTest : IClassFixture<ProductRepositoryFixture>
    {
        public readonly ProductRepositoryFixture _fixture;

        public ProcessProductControllerTest(ProductRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ProcessProductAPIController_IsCreated()
        {
            using (var context = new ApplicationDbContext(_fixture.CreateNewContextOptions()))
            {
                var repository = new ProductRepository(context, _fixture.mapper);
                var result = new ProcessProductAPIController(repository);
                Assert.IsType<ProcessProductAPIController>(result);
            }
        }

        [Theory]
        [ProcessProductRegisterData]
        public async void RegisterBreadProductionAsync_ReturnsInsertedProduct(ProcessProductDto arg, bool expected)
        {
            using (var context = new ApplicationDbContext(_fixture.CreateNewContextOptions()))
            {
                var repository = new ProductRepository(context, _fixture.mapper);
                var controller = new ProcessProductAPIController(repository);

                var response = await controller.RegisterBreadProductionAsync(arg);
                var obj = (ResponseDto)response;
                var assertion = ((ResponseDto)response).Result.GetType() == typeof(ProcessProductDto);

                Assert.Equal(expected, assertion);
            }
        }

        /*
           public int Id { get; set; }
        
        public int ProductId { get; set; }   
                
        public double Stock { get; set; }       

        public DateTime ExpirationDate { get; set; }
        public DateTime ProductionDate { get; set; }
         */

        /*
        public async Task<object> GetProductAvailableAsync(int idProduct)
        public async Task<object> UpdateProductStockAsync(double amount, int idProduct)
        */
    }
}
