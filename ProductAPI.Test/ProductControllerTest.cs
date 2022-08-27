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
    public class ProductControllerTest : IClassFixture<ProductRepositoryFixture>
    {
        public readonly ProductRepositoryFixture _fixture;

        public ProductControllerTest(ProductRepositoryFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public void ProductAPIController_ReturnsInstance()
        {
            using (var context = new ApplicationDbContext(_fixture.CreateNewContextOptions()))
            {
                var repository = new ProductRepository(context, _fixture.mapper);
                var result = new ProductAPIController(repository);
                Assert.IsType<ProductAPIController>(result);
            }
        }
        [Theory]
        [ProductGetAllData]
        public async void Get_ReturnsProductList(List<Product> data, bool expected)
        {
            using (var context = new ApplicationDbContext(_fixture.CreateNewContextOptions()))
            {
                context.Products.AddRange(data);
                context.SaveChanges();
                var repository = new ProductRepository(context, _fixture.mapper);
                var controller = new ProductAPIController(repository);
                var response = (ResponseDto)await controller.Get();

                var assertion = typeof(ProductDto) == response.Result.GetType() || typeof(List<ProductDto>) == response.Result.GetType();

                Assert.Equal(expected, assertion);
            }
        }

        [Theory]
        [ProductGetData]
        public async void GetById_ReturnsProduct(Product data, int arg, bool expected)
        {
            using (var context = new ApplicationDbContext(_fixture.CreateNewContextOptions()))
            {
                context.Products.AddRange(data);
                context.SaveChanges();
                var repository = new ProductRepository(context, _fixture.mapper);
                var controller = new ProductAPIController(repository);
                var response = (ResponseDto)await controller.Get(arg);

                var assertion = typeof(ProductDto) == response.Result.GetType();

                Assert.Equal(expected, assertion);
            }
        }

        [Theory]
        [ProductInsertData]
        public async void Post_ReturnsInsertedProduct(ProductDto expected)
        {
            using (var context = new ApplicationDbContext(_fixture.CreateNewContextOptions()))
            {
                var repository = new ProductRepository(context, _fixture.mapper);
                var controller = new ProductAPIController(repository);

                var response = await controller.Post(expected);
                var updated = (ProductDto)((ResponseDto)response).Result;

                Assert.Equal(expected.Name, updated.Name);
                Assert.Equal(expected.Price, updated.Price);
                Assert.Equal(expected.Stock, updated.Stock);
                Assert.Equal(expected.Description, updated.Description);
                Assert.Equal(expected.CategoryName, updated.CategoryName);
                Assert.Equal(expected.ImageUrl, updated.ImageUrl);
            }
        }

        [Theory]
        [ProductDeleteData]
        public async void Delete_ReturnsInsertedProduct(Product data, int arg, bool expected)
        {
            using (var context = new ApplicationDbContext(_fixture.CreateNewContextOptions()))
            {
                context.Products.Add(data);
                context.SaveChanges();
                var repository = new ProductRepository(context, _fixture.mapper);
                var controller = new ProductAPIController(repository);

                var response = await controller.Delete(arg);
                var deleted = (ResponseDto)response;
                var assertion = deleted.IsSuccess == expected;

                Assert.Equal(expected, assertion);
            }
        }

        [Theory]
        [ProductUpdateData]
        public async void Put_ReturnsUpdatedProduct(Product data, ProductDto expected)
        {

            using (var context = new ApplicationDbContext(_fixture.CreateNewContextOptions()))
            {
                context.Products.Add(data);
                context.SaveChanges();

                var repository = new ProductRepository(context, _fixture.mapper);
                var controller = new ProductAPIController(repository);
                context.ChangeTracker.Clear();

                var response = await controller.Put(expected);
                var updated = (ProductDto)((ResponseDto)response).Result;

                Assert.Equal(expected.ProductId, updated.ProductId);
                Assert.Equal(expected.Name, updated.Name);
                Assert.Equal(expected.Price, updated.Price);
                Assert.Equal(expected.Stock, updated.Stock);
                Assert.Equal(expected.Description, updated.Description);
                Assert.Equal(expected.CategoryName, updated.CategoryName);
                Assert.Equal(expected.ImageUrl, updated.ImageUrl);
            }
        }
    }
}
