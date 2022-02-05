using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lodgify.Models
{
    public class CookieTypePriceList
    {
        public int Id { get; set; }
        public double Price { get; set; }       
        public DateTime AtDate { get; set; }
    }
}
