using Moq;
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
        [Fact]
        public async void Get_CatchBadOptions()
        {
            using (var context = new ApplicationDbContext(_fixture.badOptions))
            {
                var repository = new ProductRepository(context, _fixture.mapper);
                var controller = new ProductAPIController(repository);
                var response = (ResponseDto)await controller.Get();
                Assert.False(response.IsSuccess);
                Assert.True(response.ErrorMessages.Count() > 0);
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

        [Fact]
        public async void GetById_CatchBadOptions()
        {
            using (var context = new ApplicationDbContext(_fixture.badOptions))
            {
                var repository = new ProductRepository(context, _fixture.mapper);
                var controller = new ProductAPIController(repository);
                var response = (ResponseDto)await controller.Get(It.IsAny<int>());
                Assert.False(response.IsSuccess);
                Assert.True(response.ErrorMessages.Count() > 0);
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

        [Fact]
        public async void Post_CatchBadOject()
        {
            using (var context = new ApplicationDbContext(_fixture.CreateNewContextOptions()))
            {
                var notexpected = new ProductDto() { };
                var repository = new ProductRepository(context, _fixture.mapper);
                var controller = new ProductAPIController(repository);

                var response = (ResponseDto)await controller.Post(notexpected);

                Assert.False(response.IsSuccess);
                Assert.True(response.ErrorMessages.Count() > 0);
            }
        }

        [Theory]
        [ProductDeleteData]
        public async void Delete_ReturnsBoolYesOrNot(Product data, int arg, bool expected)
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

        [Fact]
        public async void Delete_CatchNeverGetsIn()
        {
            using (var context = new ApplicationDbContext(_fixture.badOptions))
            {
                var repository = new ProductRepository(context, _fixture.badMapper);
                var controller = new ProductAPIController(repository);
                var response = (ResponseDto)await controller.Delete(It.IsAny<int>());
                //It never fails
                Assert.True(response.IsSuccess);
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

                var response = (ResponseDto)await controller.Put(expected);
                var updated = (ProductDto)response.Result;

                Assert.Equal(expected.ProductId, updated.ProductId);
                Assert.Equal(expected.Name, updated.Name);
                Assert.Equal(expected.Price, updated.Price);
                Assert.Equal(expected.Stock, updated.Stock);
                Assert.Equal(expected.Description, updated.Description);
                Assert.Equal(expected.CategoryName, updated.CategoryName);
                Assert.Equal(expected.ImageUrl, updated.ImageUrl);
            }
        }

        [Theory]
        [ProductUpdateData]
        public async void Put_Catch_EFCore_ClearChangeTrackerForUs(Product data, ProductDto expected)
        {

            using (var context = new ApplicationDbContext(_fixture.CreateNewContextOptions()))
            {
                context.Products.Add(data);
                context.SaveChanges();
                var repository = new ProductRepository(context, _fixture.mapper);
                var controller = new ProductAPIController(repository);
                var response = (ResponseDto)await controller.Put(expected);
                Assert.False(response.IsSuccess);
                Assert.True(response.ErrorMessages.Count() > 0);
            }
        }

        [Fact]
        public async void Put_CatchBadOptions()
        {
            using (var context = new ApplicationDbContext(_fixture.badOptions))
            {
                var repository = new ProductRepository(context, _fixture.mapper);
                var controller = new ProductAPIController(repository);
                var response = (ResponseDto)await controller.Put(It.IsAny<ProductDto>());
                Assert.False(response.IsSuccess);
                Assert.True(response.ErrorMessages.Count() > 0);
            }
        }
    }
}
