﻿using ProductAPI.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProductById(int productId);
        Task<ProductDto> CreateUpdateProduct(ProductDto productDto);
        Task<bool> DeleteProduct(int productId);
        Task<ProcessProductDto> RegisterBreadProductionAsync(ProcessProductDto processProductDto);

        Task<double> GetProductAvailableAsync(int idProduct);

        Task<ProductDto> UpdateProductStockAsync(double amount, int idProduct);
    }
}
