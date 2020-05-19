using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AD.Demo.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AD.Demo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly TechTestContext _context;

        public PersonController(ILogger<PersonController> logger, TechTestContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<People> Get()
        {
            return _context.People.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = _context.People.Find(id);

            if (entity != null)
            {
                return Ok(entity);
            }

            return NotFound();
        }
        
        [HttpPost]
        public IActionResult Post(People people)
        {
            if (_context.People.Find(people.PersonId) != null)
            {
                return Conflict();
            }

            _context.Add(people);
            _context.SaveChanges();
            return new StatusCodeResult(201);
        }

        [HttpPut]
        public IActionResult Put(int id, People people)
        {
            var entity = _context.People.Find(id);

            if (entity == null)
            {
                return BadRequest();
            }

            entity.FirstName = people.FirstName;
            entity.LastName = people.LastName;
            entity.IsAuthorised = people.IsAuthorised;
            entity.IsEnabled = people.IsEnabled;
            entity.IsValid = people.IsValid;

            _context.SaveChanges();
            return Ok(people);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = _context.People.Find(id);

            if (entity == null)
            {
                return BadRequest();
            }

            _context.Remove(entity);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
