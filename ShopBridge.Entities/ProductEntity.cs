using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBridge.Entities
{
    [Table(name: "Product")]
    public class ProductEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Status Status { get; set; }
        public string Image { get; set; }// only save product image path in database.
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
    public enum Status
    {
        Inactive,
        Active
    }
}
