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

        private IRepositoryStore _repoStore;
        public CookieTypesController(IRepositoryStore repoStore)
        {
            _repoStore = repoStore;
        }


        // GET: api/CookieTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CookieType>>> GetCookieType()
        {

            //Do not require type specification for constructors when the type is known.
            //reference: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/target-typed-new
            //and https://stackoverflow.com/questions/65352087/cannot-implicitly-convert-type-ienumerablet-to-actionresultienumerablet


            return new(await _repoStore.CookieType.FindAll());
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
                _repoStore.CookieType.Update(cookieType);
                await _repoStore.CookieType.Save();
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
            await _repoStore.CookieType.Save();

            return CreatedAtAction("GetCookieType", new { id = cookieType.Id }, cookieType);
        }

        // DELETE: api/CookieTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCookieType(int id)
        {
            var cookieType = await _repoStore.CookieType.Find(id);// _context.CookieType.FindAsync(id);
            if (cookieType == null)
            {
                return NotFound();
            }

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
