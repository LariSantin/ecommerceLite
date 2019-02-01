using ecommerceLite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerceLite
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>().HasKey(t => t.Id);
            
            modelBuilder.Entity<ItemCesta>().HasKey(t => t.Id);
            modelBuilder.Entity<ItemCesta>().HasOne(t => t.Pedido);
            modelBuilder.Entity<ItemCesta>().HasOne(t => t.Produto);
           
            modelBuilder.Entity<Cesta>().HasKey(t => t.Id);
            modelBuilder.Entity<Cesta>().HasMany(t => t.Itens).WithOne(t => t.Pedido);

            modelBuilder.Entity<adm>().HasKey(t => t.Id);


        }
    }
}
