using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public partial class WeatherRepository
    {
        public async Task<IEnumerable<Weather>> ListAllAsync()
        => await _context.Weather.ToArrayAsync();
    }
}