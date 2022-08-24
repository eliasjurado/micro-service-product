using ProductAPI.Models.Dtos;
using System.Reflection;
using Xunit.Sdk;

namespace ProductAPI.Test.DataAttributes
{
    public class AvailableButterStockDataAttribute : DataAttribute
    {

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] {
                new ProductDto { ProductId = 3, Name = "Butter", Price=10, Stock=100, Description = "Butter description", CategoryName="Food", ImageUrl = "Butter-url" } ,
                true
            };
            yield return new object[] {
                new ProductDto { ProductId = 3, Name = "Butter", Price=10, Stock=100, Description = "Butter description", CategoryName="Food", ImageUrl = "Butter-url" } ,
                1000f,
                false
            };
            yield return new object[] {
                new ProductDto { ProductId = 3, Name = "Butter", Price=10, Stock=100, Description = "Butter description", CategoryName="Food", ImageUrl = "Butter-url" },
                2000f,
                false
            };
        }

    }
}
