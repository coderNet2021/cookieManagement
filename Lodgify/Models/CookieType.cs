using System;
using System.Collections.Generic;

namespace Lodgify.Models
{
    public class CookieType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<CookieTypePriceList> Items { get; set; } = new List<CookieTypePriceList>();
    }
}