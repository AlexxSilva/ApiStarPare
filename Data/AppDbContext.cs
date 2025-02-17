using ApiStarPare.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiStarPare.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Carro> Carros { get; set; }
        public DbSet<Estacionamento> Estacionamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Veiculo>()
            //.HasDiscriminator<string>("Tipo")
            //.HasValue<Carro>("Carro")
            //.HasValue<Moto>("Moto");

            //modelBuilder.Ignore<Veiculo>(); // Garante que Veiculo não será salvo no banco
        }
    }
}
