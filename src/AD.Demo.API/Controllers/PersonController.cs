using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AD.Demo.API.Models;
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
        public IEnumerable<PersonModel> Get()
        {
            return _context.People
                .Include("FavouriteColours.Colour")
                .Select(p => new PersonModel
                {
                    Id = p.PersonId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    IsAuthorised = p.IsAuthorised,
                    IsEnabled = p.IsEnabled,
                    IsValid = p.IsValid,
                    Colours = p.FavouriteColours.Select(c => new ColourModel
                    {
                        Id = c.ColourId,
                        Name = c.Colour.Name,
                        IsEnabled = c.Colour.IsEnabled
                    })
                })
                .ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = _context.People
                            .Include("FavouriteColours.Colour")
                            .SingleOrDefault(p => p.PersonId == id);

            if (entity != null)
            {

                var model = new PersonModel
                {
                    Id = entity.PersonId,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    IsAuthorised = entity.IsAuthorised,
                    IsEnabled = entity.IsEnabled,
                    IsValid = entity.IsValid,
                    Colours = entity.FavouriteColours.Select(c => new ColourModel
                    {
                        Id = c.ColourId,
                        Name = c.Colour.Name,
                        IsEnabled = c.Colour.IsEnabled
                    })
                };

                return Ok(model);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Post(CreatePersonModel model)
        {
            var entity = new People()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                IsAuthorised = model.IsAuthorised,
                IsEnabled = model.IsEnabled,
                IsValid = model.IsValid,
            };

            _context.Add(entity);
            _context.SaveChanges();
            return new StatusCodeResult(201);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdatePersonModel model)
        {
            var entity = _context.People.Find(id);

            if (entity == null)
            {
                return BadRequest();
            }

            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.IsAuthorised = model.IsAuthorised;
            entity.IsEnabled = model.IsEnabled;
            entity.IsValid = model.IsValid;

            var favouriteColors = entity.FavouriteColours.ToList();
            foreach (var fc in favouriteColors)
            {
                entity.FavouriteColours.Remove(fc);
            }

            foreach (var cid in model.ColourIds ?? new int[0])
            {
                entity.FavouriteColours.Add(new FavouriteColours()
                {
                    ColourId = cid,
                });
            }

            _context.SaveChanges();
            return Ok(model);
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
