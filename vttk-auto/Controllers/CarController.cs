using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using vttk_auto.Application.Abstractions.Services;
using vttk_auto.Application.Models.Cars;
using vttk_auto.Application.Services;

namespace vttk_auto.Controllers;

[ApiController]
[Route("api/car/")]
public class CarController(ICarService carService, ParserCar parser) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddCarToDb([FromHeader] string url)
    {
        var entity = await parser.ParserCarModel();
        Console.WriteLine($"Parsed entities: {entity.Count}");

        foreach (var e in entity)
        {
            var id = await carService.CreateCar(e);
        }
        return Ok();
    }

}