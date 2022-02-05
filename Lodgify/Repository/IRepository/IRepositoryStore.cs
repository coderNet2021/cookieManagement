using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lodgify.Repository.IRepository
{
    public interface IRepositoryStore
    {
        IPerson Person { get; }
        ICookieOrder CookieOrder { get; }
        IOrderDetails OrderDetails { get; }
        ICookieType CookieType { get; }
        
    }
}
