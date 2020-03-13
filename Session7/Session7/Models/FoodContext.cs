using Microsoft.EntityFrameworkCore;

namespace Session7.Models
{
    public class FoodContext:DbContext
    {

        public DbSet<Food> Foods { get; set; }

        public FoodContext(DbContextOptions options) : base(options) { }
    }
}
