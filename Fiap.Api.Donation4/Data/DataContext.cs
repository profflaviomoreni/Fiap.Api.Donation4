using Fiap.Api.Donation4.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.Donation4.Data
{
    public class DataContext : DbContext
    {

        public DbSet<CategoriaModel> Categorias { get; set; }

        public DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Categoria
            modelBuilder.Entity<CategoriaModel>(entity => {

                entity.ToTable("Categoria");

                entity.HasKey( e => e.CategoriaId );

                entity.Property(e => e.CategoriaId).ValueGeneratedOnAdd();
                entity.Property(e => e.NomeCategoria)
                        .IsRequired()
                        .HasMaxLength(100);

                entity.HasIndex(e => e.NomeCategoria);
            });

            modelBuilder.Entity<CategoriaModel>().HasData(
                new CategoriaModel() { CategoriaId = 1, NomeCategoria = "Celular" },
                new CategoriaModel() { CategoriaId = 2, NomeCategoria = "Gadgets" }
            );
            #endregion


            #region Usuario
            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                entity.ToTable("Usuario");
                entity.HasKey(e => e.UsuarioId);

                entity.Property(e => e.UsuarioId).ValueGeneratedOnAdd();

                entity.Property(e => e.NomeUsuario)
                            .IsRequired()
                            .HasMaxLength(100);

                entity.Property(e => e.EmailUsuario)
                            .IsRequired()
                            .HasMaxLength(120);

                entity.Property(e => e.Senha)
                            .IsRequired()
                            .HasMaxLength(24);

                entity.Property(e => e.Regra)
                            .IsRequired()
                            .HasMaxLength(50);

                entity.HasIndex(e => e.EmailUsuario).IsUnique();

                entity.HasIndex( e => new {
                    e.EmailUsuario,
                    e.Senha
                });
            });


            modelBuilder.Entity<UsuarioModel>().HasData(
                new UsuarioModel(1, "admin@admin", "Admin", "admin123", "admin"),
                new UsuarioModel(2, "fmoreni@gmail.com", "Flavio Moreni", "123456", "admin")
            );
            #endregion


        }

        public DataContext() { }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

    }
}
