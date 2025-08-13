using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Carrito> Carrito { get; set; }
        public DbSet<DetallePedido> DetallePedido { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Imagen> Imagen { get; set; }
        public DbSet<Pago> Pago { get; set; }
        public DbSet<Tienda> Tienda { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.CorreoElectronico)
            .IsUnique();
        }
    }
}