using ShopBridge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.API.Model
{
    public class IsValidRequest
    {
        public static Dictionary<string,string> IsValid(ProductEntity productEntity)
        {
            Dictionary<string, string> ValidationDicObject = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(productEntity.Name))
            {
                ValidationDicObject.Add("Name", "Product name should not be null or empty");
            }
            if (string.IsNullOrEmpty(productEntity.Description))
            {
                ValidationDicObject.Add("Description", "Description should not be null or empty");
            }
            if (string.IsNullOrEmpty(productEntity.Image))
            {
                ValidationDicObject.Add("Image", "Image should not be null or empty");
            }
            Guid outGuid;
            if (!Guid.TryParse(productEntity.Id.ToString(),out outGuid))
            {
                ValidationDicObject.Add("Id", "Id should be Guid formate");
            }
            return ValidationDicObject;
        }
    }
}
