using Microsoft.EntityFrameworkCore;
using ShopBridge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.API.Model
{
    public class ShopBridgeDbContext : DbContext
    {
        public ShopBridgeDbContext(DbContextOptions<ShopBridgeDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        public DbSet<ProductEntity> Products { get; set; }
        }
}
