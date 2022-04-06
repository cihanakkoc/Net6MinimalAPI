namespace Net6MinimalAPI.Data
{
    public class CarDBContext : DbContext
    {
        public CarDBContext(DbContextOptions<CarDBContext> options) : base(options)
        {

        }

        public DbSet<Car> Cars => Set<Car>();
    }
}