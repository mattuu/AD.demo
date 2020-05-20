using System;
using System.Collections.Generic;
using System.Linq;
using AD.Demo.API.Models;
using AD.Demo.DataAccess;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AD.Demo.Services
{
    public class ColoursService : IColoursService
    {
        private readonly TechTestContext _context;
        private readonly ILogger<ColoursService> _logger;
        private readonly IMapper _mapper;

        public ColoursService(ILogger<ColoursService> logger, TechTestContext context, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IEnumerable<ColourModel> GetAll()
        {
            var query = _context.Colours
                .Where(c => c.IsEnabled);

            return _mapper.Map<IEnumerable<ColourModel>>(query);
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