using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Models;

namespace web_movie.Data
{
    public class AppDbcontext : IdentityDbContext<AppUser>
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {

        }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<ImageCinemas> Image { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<ShoppingCart_Item> ShoppingCart_Items { get; set; }
        // tạo các bảng với database
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            // bảng Actor_Movie bảng phụ của actor và movies
            modelbuilder.Entity<Actor_Movie>().HasKey(am =>
            new
            {
                am.ActorId,
                am.MovieId,
            });

            modelbuilder.Entity<Actor_Movie>().HasOne(am => am.Movies)
                .WithMany(m => m.Actors_Movies).HasForeignKey(am => am.MovieId);

            modelbuilder.Entity<Actor_Movie>().HasOne(am => am.Actors)
                .WithMany(m => m.Actors_Movies).HasForeignKey(am => am.ActorId);

            foreach (var entityType in modelbuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }
    }
}
