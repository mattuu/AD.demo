using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AD.Demo.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AD.Demo.API.Models;

namespace AD.Demo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColourController : ControllerBase
    {
        private readonly ILogger<ColourController> _logger;
        private readonly TechTestContext _context;

        public ColourController(ILogger<ColourController> logger, TechTestContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ColourModel> Get()
        {
            return _context.Colours.Select(c => new ColourModel
            {
                Id = c.ColourId,
                Name = c.Name,
                IsEnabled = c.IsEnabled
            });
        }

        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var data = _context.Colours
                .Include(c => c.FavouriteColours)
                .ToList()
                .Select(c => new
                {
                    Color = new ColourModel
                    {
                        Id = c.ColourId,
                        Name = c.Name,
                        IsEnabled = c.IsEnabled
                    },
                    Count = c.FavouriteColours.Count()
                });

            return Ok(data);
        }
    }
}