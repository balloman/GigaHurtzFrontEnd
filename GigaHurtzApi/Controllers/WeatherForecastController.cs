// using System.Net.Mime;
// using GigaHurtzApi.Services;
// using Microsoft.AspNetCore.Mvc;
//
// namespace GigaHurtzApi.Controllers;
//
// [ApiController]
// [Route("[controller]")]
// public class WeatherForecastController : ControllerBase
// {
//
//     private readonly IDbService _dbService;
//     private static readonly string[] Summaries =
//     {
//         "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//     };
//
//     private readonly ILogger<WeatherForecastController> _logger;
//
//     public WeatherForecastController(ILogger<WeatherForecastController> logger, IDbService dbService)
//     {
//         _logger = logger;
//         _dbService = dbService;
//     }
//
//     [HttpGet(Name = "GetWeatherForecast")]
//     public IEnumerable<WeatherForecast> Get()
//     {
//         var data = _dbService.GetHost("bMnCR69qgBbnd4axLBzd").Result;
//         var refugee = _dbService.GetRefugee("Xdp1szwilIlkvtOVo50s").Result;
//         Console.WriteLine(data);
//         return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//             {
//                 Date = DateTime.Now.AddDays(index),
//                 TemperatureC = Random.Shared.Next(-20, 55),
//                 Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//             })
//             .ToArray();
//     }
//
//     [HttpPost]
//     public async Task<IActionResult> Post(IFormFile file, string hostId)
//     {
//         var contentType = new ContentType(file.ContentType);
//         var result = await _dbService.UploadFile(
//             $"images/{hostId}/house",
//             file.OpenReadStream(),
//             new ContentType(file.ContentType));
//         return Ok(result);
//     }
// }
