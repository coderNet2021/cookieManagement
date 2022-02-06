using Lodgify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lodgify.DTOs
{
    public class FlatOrderDetailCookieDTO
    {
        public CookieOrder CookieOrder { get; set; }
        public OrderDetails OrderDetail { get; set; }
    }
}
