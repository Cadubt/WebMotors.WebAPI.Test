using Microsoft.EntityFrameworkCore;
using WebMotorsTest.Models;

namespace WebMotorsTest.Context
{
    public class WebMotorsDataContext : DbContext
    {
        public DbSet<AdModel> Ad { get; set; }

        public WebMotorsDataContext(DbContextOptions<WebMotorsDataContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Configure default schema
            modelBuilder.Entity<AdModel>().ToTable("tb_AnuncioWebmotors");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    }
}
