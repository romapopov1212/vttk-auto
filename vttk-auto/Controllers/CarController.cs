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
    public async Task<ActionResult> AddCarToDb([FromHeader] string url)
    {
        var entity = await parser.ParserCarModel();
        Console.WriteLine($"Parsed entities: {entity.Count}");

        foreach (var e in entity)
        {
            var id = await carService.CreateCar(e);
        }
        return Ok();
    }

    [HttpGet("brands")]
    public async Task<ActionResult<CarToGetBrands>> GetCarBrands()
    {
        var response = await carService.GetAllBrands();
        return Ok(response);
    }

    [HttpGet("models/{brand}")]
    public async Task<ActionResult<CarToGetModels>> GetCarModels(string brand)
    {
        var response = await carService.GetAllModels(brand);
        return Ok(response);
    }

    [HttpGet("modification/{brand}/{model}")]
    public async Task<ActionResult<CarToGetModifications>> GetCarModifications(string brand, string model)
    {
        var response = await carService.GetAllModifications(brand, model);
        return Ok(response);
    }

}