using Ecommerce.DataConfiguration;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce
{
    public class AplicationDbContext: DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Carrito> Carrito { get; set; }
        public DbSet<DetallePedido> DetallePedido { get; set; }

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        }
    }
}
