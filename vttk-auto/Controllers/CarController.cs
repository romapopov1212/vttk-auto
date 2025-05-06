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
        var entity =  parser.ParserCarModel();
        Console.WriteLine($"Parsed entities: {entity.Count}");

        foreach (var e in entity)
        {
            var id = await carService.CreateCar(e);
        }
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CarToGetById>> GetCarById(Guid id)
    {
        var car = await carService.GetById(id);
        if (car is null)
        {
            return NotFound();
        }
        return Ok(car);
    }

    [HttpGet]
    public async Task<ActionResult<List<CarToGetAll>>> GetCars()
    {
        var cars = await carService.GetAllCars();
        if (cars is null)
        {
            return NotFound();
        }
        
        return Ok(cars);
    }
    
    [HttpGet("brands")]
    public async Task<ActionResult<CarToGetBrands>> GetCarBrands()
    {
        var response = await carService.GetAllBrands();
        
        if (response is null)
        {
            return NotFound();
        }
        
        return Ok(response);
    }

    [HttpGet("models/{brand}")]
    public async Task<ActionResult<CarToGetModels>> GetCarModels(string brand)
    {
        var response = await carService.GetAllModels(brand);
        
        if (response is null)
        {
            return NotFound();
        }
        
        return Ok(response);
    }

    [HttpGet("modification/{brand}/{model}")]
    public async Task<ActionResult<CarToGetModifications>> GetCarModifications(string brand, string model)
    {
        var response = await carService.GetAllModifications(brand, model);

        if (response is null)
        {
            return NotFound();
        }
        
        return Ok(response);
    }

}