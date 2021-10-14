using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.API.Model;
using ShopBridge.API.Services;
using ShopBridge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            List<ProductEntity> productEntities = new List<ProductEntity>();
            ResponseEntity response = new ResponseEntity();
            try
            {
                productEntities = _productService.GetProducts().GetAwaiter().GetResult();
                response.status = true;
                response.data = productEntities;
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }           
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult AddOrEditProduct(ProductEntity productEntity)
        {
            ProductEntity product = new ProductEntity();
            ResponseEntity response = new ResponseEntity();
            try
            {
                if (productEntity == null)
                {
                    response.status = false;
                    return BadRequest(response);
                }
                Dictionary<string, string> validations = IsValidRequest.IsValid(productEntity);
                if (validations.Count == 0)
                {
                    product = _productService.AddOrEditProduct(productEntity).GetAwaiter().GetResult();
                    response.status = true;
                    response.data = product;
                    return Ok(response);
                }
                else
                {
                    response.status = false;
                    response.errors = validations;
                    return BadRequest(response);
                }
                    
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
        
        [HttpDelete]
        [Route("[action]/{productId}")]
        public IActionResult DeleteProduct(string productId)
        {
            ProductEntity product = new ProductEntity();
            ResponseEntity response = new ResponseEntity();
            try
            {
                if (string.IsNullOrEmpty(productId))
                {
                    response.status = false;
                    response.errors = new Dictionary<string, string>();
                    response.errors.Add("productId", "productId should not be null or empty.");
                    return BadRequest(response);
                }
                Guid result;
                if (Guid.TryParse(productId, out result))
                {
                    product = _productService.DeleteProduct(productId).GetAwaiter().GetResult();
                    if (product != null)
                    {
                        response.status = true;
                        response.data = product;
                        return Ok(response);
                    }
                }               
                response.status = false;
                response.errors = new Dictionary<string, string>();
                response.errors.Add("productId", "Record not found for Product Id:" + productId);
                return NotFound(response);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
    }
}
