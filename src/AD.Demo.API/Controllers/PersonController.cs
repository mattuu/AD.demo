using System;
using System.Collections.Generic;
using AD.Demo.API.Models;
using AD.Demo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AD.Demo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPeopleService _peopleService;

        public PersonController(ILogger<PersonController> logger, IPeopleService peopleService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _peopleService = peopleService ?? throw new ArgumentNullException(nameof(peopleService));
        }

        [HttpGet]
        public IEnumerable<PersonModel> Get()
        {
            return _peopleService.GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_peopleService.Find(id));
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreatePersonModel model)
        {
            return Created(HttpContext.Request.Path, _peopleService.Create(model));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdatePersonModel model)
        {
            try
            {
                return Ok(_peopleService.Update(id, model));
            }
            catch (EntityNotFoundException)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return BadRequest();
            }
        }
    }
}