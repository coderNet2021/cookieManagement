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

namespace Lodgify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CookieTypesController : ControllerBase
    {

        private readonly IRepositoryStore _repoStore;
        //private readonly cookiesContext _context;
        public CookieTypesController(IRepositoryStore repoStore /*, cookiesContext context*/)
        {
            _repoStore = repoStore;
           // _context = context;
        }




        // GET: api/CookieTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CookieType>>> GetCookieType()
        {

            //Do not require type specification for constructors when the type is known.
            //reference: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/target-typed-new
            //and https://stackoverflow.com/questions/65352087/cannot-implicitly-convert-type-ienumerablet-to-actionresultienumerablet

            //get all the cookie types and their list of price from the other table cookieTypePriceList
            var result = await _repoStore.CookieType.FindAll(includes: q => q.Include(x => x.Items));

            if (result == null) return BadRequest(new { message = "Bad Request of cookie history" });

            if (result.Count() == 0) return NotFound(new { message = "cookie not found!" });

            return new(result);
        }




        [HttpGet("cookiTypePrices/{id}")]
        public async Task<ActionResult<IEnumerable<CookieType>>> GetCookiePriceHistory(int id)
        {
            var result = await _repoStore.CookieType.FindAll(u => u.Id == id, includes: q => q.Include(x => x.Items));

            if (result == null) return BadRequest(new { message = "Bad Request of cookie history" });
            var items = result.Select(x => new { items = x.Items });

            if (items.Count() == 0 || items==null) return NotFound(new { message = "cookie and Price History not found!" });

            return new(result);
        }




        // GET: api/CookieTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CookieType>> GetCookieType(int id)
        {
            var cookieType = await _repoStore.CookieType.Find(id);//_ICookieTypesServices.GetById(id);

            if (cookieType == null)
            {
                return NotFound();
            }

            return cookieType;
        }




        // PUT: api/CookieTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCookieType(int id, CookieType cookieType)
        {
            if (id != cookieType.Id)
            {
                return BadRequest();
            }

            try
            {
                //updating the current price of the cookie type
                //and also entering new row in the PriceList history for the cookie type
                _repoStore.CookieType.Update(cookieType);
                
                CookieTypePriceList aCookieTypePriceObject = new CookieTypePriceList();
                aCookieTypePriceObject.AtDate = DateTime.Now;
                aCookieTypePriceObject.Price = cookieType.Price;

                cookieType.Items.Add(aCookieTypePriceObject);

                await _repoStore.CookieTypePriceList.Add(aCookieTypePriceObject);

                await _repoStore.CookieType.Save();
                await _repoStore.CookieTypePriceList.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CookieTypeExists(id))
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




        // POST: api/CookieTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CookieType>> PostCookieType(CookieType cookieType)
        {
            await _repoStore.CookieType.Add(cookieType);


            CookieTypePriceList aCookieTypePriceObject = new CookieTypePriceList();
            aCookieTypePriceObject.AtDate = DateTime.Now;
            aCookieTypePriceObject.Price = cookieType.Price;

            cookieType.Items.Add(aCookieTypePriceObject);

            await _repoStore.CookieTypePriceList.Add(aCookieTypePriceObject);

            await _repoStore.CookieType.Save();
            await _repoStore.CookieTypePriceList.Save();

            return CreatedAtAction("GetCookieType", new { id = cookieType.Id }, cookieType);
        }




        // DELETE: api/CookieTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCookieType(int id)//Not Working delete constraint exception
        {
            var cookieType = await _repoStore.CookieType.Find(id); //await _context.CookieType.Include("CookieTypePriceList").Where(x=>x.Id==id).FirstOrDefaultAsync(); ////

            if (cookieType == null)
            {
                return NotFound();
            }
            
            //foreach (var item in cookieType.Items)
            //{
            //    _repoStore.CookieTypePriceList.Remove(item);
            //    await _repoStore.CookieTypePriceList.Save();
            //}

            _repoStore.CookieTypePriceList.RemoveRange(cookieType.Items);
            await _repoStore.CookieTypePriceList.Save();

            _repoStore.CookieType.Remove(cookieType);
            await _repoStore.CookieType.Save();

            return NoContent();
        }


        private async Task<bool> CookieTypeExists(int id)
        {
            var res= await _repoStore.CookieType.FindAll();
            return res.Any(el => el.Id == id);
        }
    }
}
