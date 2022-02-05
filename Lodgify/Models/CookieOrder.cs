using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lodgify.Models
{
    public class CookieOrder
    {
        public int Id { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public List<OrderDetails> Items { get; set; } = new List<OrderDetails>();
        public double TotalAmount { get; set; }

        //nav
        public long PersonId { get; set; }
        public Person Person { get; set; }
    }
}
