using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AD.Demo.API.Models;
using AD.Demo.Services;

namespace AD.Demo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColourController : ControllerBase
    {
        private readonly ILogger<ColourController> _logger;
        private readonly IColoursService _coloursService;

        public ColourController(ILogger<ColourController> logger, IColoursService coloursService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _coloursService = coloursService ?? throw new ArgumentNullException(nameof(coloursService));
        }

        [HttpGet]
        public IEnumerable<ColourModel> Get()
        {
            return _coloursService.GetAll();
        }

        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var data = _coloursService.GetStats();
            return Ok(data);
        }
    }
}