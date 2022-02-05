using Lodgify.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lodgify.Data
{
    public class cookiesContext: DbContext
    {
        public cookiesContext(DbContextOptions<cookiesContext> options) : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<CookieOrder> CookieOrder { get; set; }
        public DbSet<CookieType> CookieType { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<CookieTypePriceList> CookieTypePriceList { get; set; }


    }
}
