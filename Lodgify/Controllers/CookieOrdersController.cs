using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lodgify.Data;
using Lodgify.Models;
using Lodgify.Repository.IRepository;
using Lodgify.DTOs;
using Lodgify.Utility;

namespace Lodgify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CookieOrdersController : ControllerBase
    {
        private readonly IRepositoryStore _repoStore;
        private readonly cookiesContext _context;


        public CookieOrdersController(IRepositoryStore repoStore, cookiesContext context)
        {
            _repoStore = repoStore;
            _context = context;
        }



        // GET: api/CookieOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CookieOrder>>> GetCookieOrder()
        {
            return new(await _repoStore.CookieOrder.FindAll(includes: q=>q.Include(x=>x.Items)));
        }
        
        

        [HttpGet("{month}/{year}")]
        public async Task<ActionResult<IEnumerable<CookieOrder>>> GetCookieOrderMY(int month,int year)
        {
            return new(await _repoStore.CookieOrder.FindAll(u=>u.CreatedAt.Month==month && u.CreatedAt.Year==year,includes: q => q.Include(x => x.Items)));
        }



        [HttpGet("{id}/{month}/{year}")]
        public async Task<ActionResult<IEnumerable<CookieOrder>>> GetCookieOrderPMY(int id,int month, int year)
        {
            return new(await _repoStore.CookieOrder.FindAll(u => u.CreatedAt.Month == month && u.CreatedAt.Year == year && u.PersonId==id, includes: q => q.Include(x => x.Items)));
        }


        [HttpGet("cookiType/{id}/{month}/{year}")]
        public async Task<ActionResult<IEnumerable<OrderDetails>>> GetCookieTypeTMY(int id, int month, int year)
        {


            return _context.OrderDetails.Include("CookieOrder").Include("CookieType").Where(a=>a.CookieTypeId==id ).ToList() ;

        }


        // GET: api/CookieOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CookieOrder>> GetCookieOrder(int id)
        {
            var cookieOrder = await _repoStore.CookieOrder.Find(id);

            if (cookieOrder == null)
            {
                return NotFound();
            }

            return cookieOrder;
        }



        // PUT: api/CookieOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCookieOrder(int id, CookieOrder cookieOrder)
        {
            if (id != cookieOrder.Id)
            {
                return BadRequest();
            }

            try
            {
                _repoStore.CookieOrder.Update(cookieOrder);
                await _repoStore.CookieOrder.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CookieOrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        
        
        // POST: api/CookieOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CookieOrder>> PostCookieOrder(CookieOrderDetailsDTO cookieOrderDetailsDto)
        {
            

            List<OrderDetails> anOrderDetailsList = new List<OrderDetails>();
            double TotalAmount = 0.0;

            foreach (var anOrderDetail in cookieOrderDetailsDto.OrderDetails)
            {
                OrderDetails od = new OrderDetails();
                CookieType aCookietype = new CookieType();

                od = anOrderDetail;
                anOrderDetailsList.Add(od);
                await _repoStore.OrderDetails.Add(od);

                aCookietype = await _repoStore.CookieType.Find(od.CookieTypeId);

                TotalAmount += od.Quantity * aCookietype.Price;
            }

            cookieOrderDetailsDto.CookieOrder.Items = anOrderDetailsList;
            cookieOrderDetailsDto.CookieOrder.TotalAmount = TotalAmount;

            await _repoStore.CookieOrder.Add(cookieOrderDetailsDto.CookieOrder);

            if ((await TotalCumulativeForMonth(DateTime.Now)) + TotalAmount <= Constants.Budget)
            {
                await _repoStore.CookieOrder.Save();
                await _repoStore.OrderDetails.Save();

                CookieOrderDetailsDTO aCookieOrderDetailsDto = new CookieOrderDetailsDTO();
                aCookieOrderDetailsDto = cookieOrderDetailsDto;
                await WriteToFile(aCookieOrderDetailsDto);

                return CreatedAtAction("GetCookieOrder", new { id = cookieOrderDetailsDto.CookieOrder.Id }, cookieOrderDetailsDto.CookieOrder);
            }

            else return BadRequest(new { message = Constants.Budget+ "$ has been reached, if added to your current order, plz make the quantities fewer, or delete some cookies " });
        }

        
        
        // DELETE: api/CookieOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCookieOrder(int id)
        {
            var cookieOrder = await _repoStore.CookieOrder.Find(id);
            if (cookieOrder == null)
            {
                return NotFound();
            }

            _repoStore.CookieOrder.Remove(cookieOrder);
            await _repoStore.CookieOrder.Save();

            return NoContent();
        }

        private async Task<bool> CookieOrderExists(int id)
        {
            var res = await _repoStore.CookieType.FindAll();
            return res.Any(el => el.Id == id);
        }

        private async Task<double> TotalCumulativeForMonth(DateTime date) 
        {
            double result = 0.0;
            List<CookieOrder> cookieOerderList = new List<CookieOrder>();

            cookieOerderList = new(await _repoStore.CookieOrder.FindAll(u => u.CreatedAt.Year == date.Year && u.CreatedAt.Month == date.Month));

            foreach (var item in cookieOerderList)
            {
                result += item.TotalAmount;
            }

            return result;
        }

        private async Task WriteToFile(CookieOrderDetailsDTO cookiOrderDetailsDto)//cookiOrderDetailsDto.CookieOrder.Person.Id.ToString(),
        {
            string[] lines =
       {
             " has ordered", "yuyuy"
        };
            await System.IO.File.WriteAllLinesAsync(@"C:\LodgifyOrders\Order"+DateTime.Now.ToShortDateString()+".txt", lines);
        }
    }
}
