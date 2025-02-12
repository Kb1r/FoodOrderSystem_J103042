using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FoodOrderSystem_J103042.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; 
using Microsoft.AspNetCore.Identity; 

namespace FoodOrderSystem_J103042.Data
{
   
    public class FoodOrderSystem_J103042Context : IdentityDbContext<IdentityUser>
    {
        public FoodOrderSystem_J103042Context(DbContextOptions<FoodOrderSystem_J103042Context> options)
            : base(options)
        {
        }

        public DbSet<FoodOrderSystem_J103042.Models.FoodItem> FoodItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            // Configure the FoodItem entity
            modelBuilder.Entity<FoodItem>().ToTable("FoodItems");

            // Optional: Seed initial data (if needed)
            modelBuilder.Entity<FoodItem>().HasData(
                new FoodItem
                {
                    ID = 1,
                    Item_Name = "Mohinga",
                    Item_Desc = "Traditional Burmese fish noodle soup.",
                    Price = 5.99m,
                    Available = true,
                    Vegetarian = false,
                    ImageUrl = "/images/mohinga.jpg"
                },
                new FoodItem
                {
                    ID = 2,
                    Item_Name = "Laphet Thoke",
                    Item_Desc = "Burmese tea leaf salad.",
                    Price = 4.99m,
                    Available = true,
                    Vegetarian = true,
                    ImageUrl = "/images/laphet-thoke.jpg"
                },
                new FoodItem
                {
                    ID = 3,
                    Item_Name = "Ohn No Khao Swe",
                    Item_Desc = "Coconut chicken noodle soup.",
                    Price = 6.99m,
                    Available = true,
                    Vegetarian = false,
                    ImageUrl = "/images/ohn-no-khao-swe.jpg"
                }
            );
        }
    }
}