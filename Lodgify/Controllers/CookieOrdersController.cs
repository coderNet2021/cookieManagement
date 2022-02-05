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

namespace Lodgify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CookieOrdersController : ControllerBase
    {
        private readonly IRepositoryStore _repoStore;

        public CookieOrdersController(IRepositoryStore repoStore)
        {
            _repoStore = repoStore;
        }

        // GET: api/CookieOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CookieOrder>>> GetCookieOrder()
        {
            return new(await _repoStore.CookieOrder.FindAll());
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

            foreach (var anOrderDetail in cookieOrderDetailsDto.OrderDetails)
            {
                OrderDetails od = new OrderDetails();
                od = anOrderDetail;
                anOrderDetailsList.Add(od);
                await _repoStore.OrderDetails.Add(od);
            }

            cookieOrderDetailsDto.CookieOrder.Items = anOrderDetailsList;

            await _repoStore.CookieOrder.Add(cookieOrderDetailsDto.CookieOrder);

            await _repoStore.CookieOrder.Save();
            await _repoStore.OrderDetails.Save();

           



            return CreatedAtAction("GetCookieOrder", new { id = cookieOrderDetailsDto.CookieOrder.Id }, cookieOrderDetailsDto.CookieOrder);
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
    }
}
