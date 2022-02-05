using Lodgify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lodgify.DTOs
{
    public class CookieOrderDetailsDTO
    {
        public CookieOrder CookieOrder { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
