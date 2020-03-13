using Microsoft.AspNetCore.Mvc;
using Session7.Models;
using System.Threading.Tasks;

namespace Session7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly FoodContext db;

        public FoodController(FoodContext context)
        {
            db = context;
        }
             

        // Get department with given id.
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFood(int id)
        {
            var food = await db.Foods.FindAsync(id);
            if (food == default(Food))
            {
                return NotFound();
            }
            return Ok(food);
        }

        // Add a department to db.
        [HttpPost]
        public async Task<IActionResult> AddFood([FromBody] Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await db.AddAsync(food);
            await db.SaveChangesAsync();
            return Ok(food.Id);
        }
    }
}