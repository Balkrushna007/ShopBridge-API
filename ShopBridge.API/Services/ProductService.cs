using ShopBridge.API.Model;
using ShopBridge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.API.Services
{
    public class ProductService : IProductService
    {
        private readonly ShopBridgeDbContext _DbContext;

        public ProductService(ShopBridgeDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<ProductEntity> AddOrEditProduct(ProductEntity productEntity)
        {
            //first check if product exists or not based on ProductId if exist then update record otherwise add new record in product table
            ProductEntity product = _DbContext.Products.FirstOrDefault(x => x.Id == productEntity.Id);
            if (product == null)
            {
                #region Added new record in Product table
                productEntity.CreatedAt = DateTime.Now;
                productEntity.Status = Status.Active;
                _DbContext.Products.Add(productEntity);               
                await _DbContext.SaveChangesAsync();
                #endregion
            }
            else
            {
                #region Update existing record in Product table
                product.CreatedAt = productEntity.CreatedAt;
                product.Description = productEntity.Description;
                product.Id = productEntity.Id;
                product.Name = productEntity.Name;
                product.ShortDescription = productEntity.ShortDescription;
                product.Status = Entities.Status.Active;
                product.Price = productEntity.Price;
                product.Quantity = productEntity.Quantity;
                product.Image = productEntity.Image;
                product.UpdateAt = DateTime.Now;
                _DbContext.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _DbContext.SaveChangesAsync();
                #endregion
            }
            
            return productEntity;
        }

        public async Task<ProductEntity> DeleteProduct(string productId)
        {
            ProductEntity productEntity = new ProductEntity();
            productEntity = _DbContext.Products.FirstOrDefault(x => x.Id.Equals(Guid.Parse(productId)));
            if (productEntity != null)
            {
                _DbContext.Products.Remove(productEntity);
                await _DbContext.SaveChangesAsync();
            }
            return productEntity;
        }

        public async Task<List<ProductEntity>> GetProducts()
        {
            List<ProductEntity> productEntities = new List<ProductEntity>();
            productEntities = _DbContext.Products.ToList();
            return await Task.FromResult(productEntities);
        }
    }
}
