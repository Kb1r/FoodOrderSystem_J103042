using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FoodOrderSystem_J103042.Models;

namespace FoodOrderSystem_J103042.Data
{
    public class FoodOrderSystem_J103042Context : DbContext
    {
        public FoodOrderSystem_J103042Context(DbContextOptions<FoodOrderSystem_J103042Context> options)
            : base(options)
        {
        }

        public DbSet<FoodOrderSystem_J103042.Models.FoodItem> FoodItems { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodItem>().ToTable("FoodItems");
        }
    }
}
