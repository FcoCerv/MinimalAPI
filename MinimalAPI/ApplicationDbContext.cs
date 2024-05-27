using Microsoft.EntityFrameworkCore;
using MinimalAPI.Entidades;
using MinimalAPI.Migrations;
using OCRD_SalesForce = MinimalAPI.Entidades.OCRD_SalesForce;

namespace MinimalAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<OCRD_SalesForce> OCRDs { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }

        //Se crea un metodo para crear el api fluente
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Paciente>().Property(p => p.Prospecto).HasMaxLength(50);
        }
    }
}
