using Lodgify.Data;
using Lodgify.Models;
using Lodgify.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lodgify.Repository.RepositoryImplementation
{
    public class CookiOerderRepository : RepositoryBase<CookieOrder>, ICookieOrder
    {
        public CookiOerderRepository(cookiesContext context) : base(context)
        {
        }
    }
}
