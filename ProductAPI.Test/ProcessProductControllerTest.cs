using ProductAPI.Controllers;
using ProductAPI.DbContexts;
using ProductAPI.Models;
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
                var assertion = ((ResponseDto)response).Result.GetType() == typeof(ProcessProductDto);

                Assert.Equal(expected, assertion);
            }
        }
        [Theory]
        [ProcessProductStockGetData]
        public async void GetProductAvailableAsync_ReturnsQuantity(Product data, int input_idProduct, int output_quantity, bool expected)
        {
            using (var context = new ApplicationDbContext(_fixture.CreateNewContextOptions()))
            {
                context.Products.Add(data);
                context.SaveChanges();
                var repository = new ProductRepository(context, _fixture.mapper);
                var controller = new ProcessProductAPIController(repository);

                var response = await controller.GetProductAvailableAsync(input_idProduct);
                var assertion = (double)((ResponseDto)response).Result == output_quantity;

                Assert.Equal(expected, assertion);
            }
        }

        [Theory]
        [ProcessProductStockUpdateData]
        public async void UpdateProductStockAsync_ReturnsQuantity(Product data, double input_amount, int input_idProduct, double output_Stock, bool expected)
        {
            using (var context = new ApplicationDbContext(_fixture.CreateNewContextOptions()))
            {
                context.Products.Add(data);
                context.SaveChanges();
                var repository = new ProductRepository(context, _fixture.mapper);
                var controller = new ProcessProductAPIController(repository);

                var response = await controller.UpdateProductStockAsync(input_amount, input_idProduct);
                var result = (ProductDto)((ResponseDto)response).Result;
                var assertion = result.Stock == output_Stock;

                Assert.Equal(expected, assertion);
            }
        }
    }
}
