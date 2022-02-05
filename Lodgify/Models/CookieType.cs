using System;

namespace Lodgify.Models
{
    public class CookieType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime AtDate { get; set; }
    }
}