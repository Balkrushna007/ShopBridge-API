using ShopBridge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.API.Services
{
    public interface IProductService
    {
        Task<List<ProductEntity>> GetProducts();
        Task<ProductEntity> AddOrEditProduct(ProductEntity productEntity);
        Task<ProductEntity> DeleteProduct(string productId);
    }
}
