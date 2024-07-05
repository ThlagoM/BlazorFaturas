using BlazorFatura.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace BlazorFatura.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Fatura> Faturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações de modelo, como chaves primárias, índices, etc.
            modelBuilder.Entity<Fatura>()
                .HasKey(f => f.idFatura); // Define a chave primária
        }

    }


}
