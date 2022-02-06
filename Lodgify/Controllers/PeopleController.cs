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
    public class PeopleController : ControllerBase
    {
        private readonly IRepositoryStore _repoStore;

        public PeopleController(IRepositoryStore repoStore)
        {
            _repoStore = repoStore;
        }




        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
        {
            var result = await _repoStore.Person.FindAll();

            if (result == null) return BadRequest(new { message = "Bad Request of Persons" });

            if (result.Count() == 0) return NotFound(new { message = "Persons not found!" });

            return new(result);
        }




        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _repoStore.Person.Find(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }




        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            try
            {
                _repoStore.Person.Update(person);
                await _repoStore.Person.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PersonExists(id))
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




        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            await _repoStore.Person.Add(person);
            await _repoStore.Person.Save();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }




        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _repoStore.Person.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            _repoStore.Person.Remove(person);
            await _repoStore.Person.Save();

            return NoContent();
        }




        private async Task<bool> PersonExists(int id)
        {
            var res = await _repoStore.Person.FindAll();
            return res.Any(el => el.Id == id);
        }
    }
}
