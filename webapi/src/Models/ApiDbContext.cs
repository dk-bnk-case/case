using Microsoft.EntityFrameworkCore;
using webapi.Maps;

namespace webapi.Models {

    public class ApiDbContext : DbContext {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<Municipality> Municipalitys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            new MunicipalityMap(modelBuilder.Entity<Municipality>());
        }
    }
}