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

        [HttpPost]
        public IActionResult Post(People people)
        {
            if(_context.People.Find(people.PersonId) != null){
                return Conflict();
            }

            _context.Add(people);
            _context.SaveChanges();
            return new StatusCodeResult(201);
        }
    }
}
