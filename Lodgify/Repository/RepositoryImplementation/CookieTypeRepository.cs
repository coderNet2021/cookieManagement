using Lodgify.Data;
using Lodgify.Models;
using Lodgify.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lodgify.Repository.RepositoryImplementation
{
    public class CookieTypeRepository : RepositoryBase<CookieType>, ICookieType
    {
        public CookieTypeRepository(cookiesContext context) : base(context)
        {
        }
    }
}
