using System.ComponentModel.DataAnnotations.Schema;
using API.DTOs;
using Common;

namespace API.Entities
{
    [Table("Weather")]
    [GenerateController(typeof(WeatherDto))]
    public class Weather : BaseEntity
    {
        public string Summary { get; set; }
    }
}