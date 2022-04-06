using Microsoft.AspNetCore.Mvc;
using Net6MinimalAPI.Data;

namespace Net6MinimalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarDBContext _carDBContext;

        public CarController(CarDBContext carDBContext)
        {
            _carDBContext = carDBContext;
        }

        [HttpPost]
        public async Task<ActionResult<List<Car>>> CreateCar(Car car)
        {
            _carDBContext.Cars.Add(car);
            await _carDBContext.SaveChangesAsync();
            return Ok(await _carDBContext.Cars.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Car>>> DeleteCar(int id)
        {
            var dbGame = await _carDBContext.Cars.FindAsync(id);
            if (dbGame == null)
                return NotFound("No car.");

            _carDBContext.Cars.Remove(dbGame);
            await _carDBContext.SaveChangesAsync();

            return Ok(await _carDBContext.Cars.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetCars()
        {
            return Ok(await _carDBContext.Cars.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Car>>> GetCars(int id)
        {
            var game = await _carDBContext.Cars.FindAsync(id);
            if (game == null)
                return NotFound("No car.");
            return Ok(game);
        }

        [HttpPut]
        public async Task<ActionResult<List<Car>>> UpdateVideoGame(Car car, int id)
        {
            var _car = await _carDBContext.Cars.FindAsync(id);
            if (_car == null)
                return NotFound("No car.");

            _car.Year = car.Year;
            _car.Brand=car.Brand;
            _car.Model = car.Model;

            await _carDBContext.SaveChangesAsync();
            return Ok(await _carDBContext.Cars.ToListAsync());
        }
    }
}