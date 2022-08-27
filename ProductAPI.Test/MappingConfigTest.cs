using AutoMapper;
using Xunit;

namespace ProductAPI.Test
{
    public class MappingConfigTest
    {
        [Fact]
        public void RegisterMaps_IsCreated()
        {
            var config = MappingConfig.RegisterMaps();
            Assert.IsType<MapperConfiguration>(config);
        }
    }
}
