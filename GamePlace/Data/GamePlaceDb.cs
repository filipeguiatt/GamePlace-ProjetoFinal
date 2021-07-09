using GamePlace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GamePlace.Data
{
    public class GamePlaceDb : IdentityDbContext
    {
        public GamePlaceDb(DbContextOptions<GamePlaceDb> options)
            : base(options)
        {
        }


        /// <summary>
        /// método para assistir a criação da base de dados que representa o modelo
        /// </summary>
        /// <param name="modelBuilder">opção de configuração da criação do modelo</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // importar todo o comportamento deste método
            // definido na classe DbContext
            base.OnModelCreating(modelBuilder);


            //*********************************************************************
            // acrescentar novos dados às tabelas - seed das tabelas
            //*********************************************************************

            // adicionar os Roles
            modelBuilder.Entity<IdentityRole>().HasData(
               new IdentityRole { Id = "u", Name = "Utilizador", NormalizedName = "UTILIZADOR" },
               new IdentityRole { Id = "a", Name = "Admin", NormalizedName = "ADMIN" }
            );
        }
        public DbSet<UtilizadorRegistado> UtilizadorRegistado { get; set; }
        public DbSet<GamePlace.Models.Jogos> Jogos { get; set; }
        public DbSet<GamePlace.Models.Recursos> Recursos { get; set; }
        public DbSet<GamePlace.Models.Compras> Compras { get; set; }
    }
}
