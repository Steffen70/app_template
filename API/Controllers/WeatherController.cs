using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public partial class WeatherController
    {
        [Authorize(Policy = "RequireModeratorRole")]
        [HttpGet("Forecast")]
        public async Task<IEnumerable<WeatherForecastDto>> GetForecast()
        {
            var weather = await _unitOfWork.WeatherRepository.ListAllAsync();
            var summaries = weather.Select(w => w.Summary).ToArray();

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecastDto
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = summaries[rng.Next(summaries.Length)]
            })
            .ToArray();
        }
    }
}
