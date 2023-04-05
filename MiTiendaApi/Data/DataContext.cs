using Microsoft.EntityFrameworkCore;
using MiTiendaApi.Models.Entities;

namespace MiTiendaApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Cliente> Clientes { get; set;}
        public DbSet<Producto> Productos { get; set;}
        public DbSet<Proveedor> Proveedores { get; set;}
        public DbSet<ClienteProducto> ClientesProductos { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Nombre).IsRequired();
                entity.Property(c => c.Apellido).IsRequired();
                entity.Property(c => c.Direccion).IsRequired();
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Nombre).IsRequired();
                entity.Property(p => p.Direccion).IsRequired();

                entity.HasMany(p => p.Productos).WithOne(p => p.Proveedor).HasForeignKey(p => p.ProveedorId);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Nombre).IsRequired();
                entity.Property(p => p.Precio).IsRequired();

                entity.HasOne(p => p.Proveedor).WithMany(p => p.Productos).HasForeignKey(p => p.ProveedorId);

                entity
                    .HasMany(e => e.Clientes)
                    .WithMany(p => p.Productos)
                    .UsingEntity<ClienteProducto>(
                        l => l.HasOne<Cliente>().WithMany(e => e.ClientesProductos),
                        r => r.HasOne<Producto>().WithMany(e => e.ClientesProductos));
            });

        }

    }
}
