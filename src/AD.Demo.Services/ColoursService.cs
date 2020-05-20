using System;
using System.Collections.Generic;
using System.Linq;
using AD.Demo.API.Models;
using AD.Demo.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AD.Demo.Services
{
    public class ColoursService : IColoursService
    {
        private readonly ILogger<ColoursService> _logger;
        private readonly TechTestContext _context;

        public ColoursService(ILogger<ColoursService> logger, TechTestContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<ColourModel> GetAll()
        {
            return _context.Colours
            .Where(c => c.IsEnabled)
            .Select(c => new ColourModel
            {
                Id = c.ColourId,
                Name = c.Name
            });
        }

        public IEnumerable<ColourStatsModel> GetStats()
        {

            var data = _context.Colours
                          .Include(c => c.FavouriteColours)
                          .ToList()
                          .Select(c => new ColourStatsModel
                          {
                              Id = c.ColourId,
                              Name = c.Name,
                              Count = c.FavouriteColours.Count()
                          });

            return data;
        }
    }
}
