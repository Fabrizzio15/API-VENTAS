using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        
    }
}