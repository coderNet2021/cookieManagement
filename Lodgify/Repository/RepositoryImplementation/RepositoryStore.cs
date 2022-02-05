using Lodgify.Data;
using Lodgify.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lodgify.Repository.RepositoryImplementation
{
    public class RepositoryStore : IRepositoryStore
    {
        private cookiesContext _context;
        private IPerson _person;
        private ICookieOrder _cookieOrder;
        private ICookieType _cookieType;
        private IOrderDetails _orderDetails;
        public IPerson Person
        {
            get
            {
                if (_person == null)
                {
                    _person = new PersonRepository(_context);
                }
                return _person;
            }
        }
        public ICookieOrder CookieOrder
        {
            get
            {
                if (_cookieOrder == null)
                {
                    _cookieOrder = new CookiOerderRepository(_context);
                }
                return _cookieOrder;
            }
        }
        public ICookieType CookieType
        {
            get
            {
                if (_cookieType == null)
                {
                    _cookieType = new CookieTypeRepository(_context);
                }
                return _cookieType;
            }
        }
        public IOrderDetails OrderDetails
        {
            get
            {
                if (_orderDetails == null)
                {
                    _orderDetails = new OrderDetailsRepository(_context);
                }
                return _orderDetails;
            }
        }

        public RepositoryStore(cookiesContext context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
