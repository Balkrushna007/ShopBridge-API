using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Entities
{
    public class ResponseEntity
    {
        public bool status { get; set; }
        public object data { get; set; }
        public Dictionary<string,string> errors { get; set; }
    }
}
