namespace PTW.Domain.Storage.Common.Context
{
    using Microsoft.EntityFrameworkCore;
    using PTW.Domain.Storage.Common.Configuration;

    public class PTWDbContext : DbContext, IPTWDbContext
    {
        public PTWDbContext(DbContextOptions<PTWDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ForecastMapping());
        }

        public int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
