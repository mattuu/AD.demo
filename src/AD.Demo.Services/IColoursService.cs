using System.Collections.Generic;
using AD.Demo.API.Models;

namespace AD.Demo.Services
{
    public interface IColoursService
    {
        IEnumerable<ColourModel> GetAll();

        IEnumerable<ColourStatsModel> GetStats();
    }
}
