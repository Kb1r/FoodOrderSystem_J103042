using FoodOrderSystem_J103042.Models; 
using Microsoft.EntityFrameworkCore;

namespace FoodOrderSystem_J103042.Data 
{
    public class DbInitializer
    {
        public static void Initialize(FoodOrderSystem_J103042Context context)
        {
            if (context.FoodItems.Any())
            {
                return; 
            }

            var foodItems = new FoodItem[]
            {
                new FoodItem { Item_Name = "Shepherd's Pie", Item_Desc = "Our tasty shepherd's pie packed full of lean minced lamb and an assortment of vegetables", Available = true, Vegetarian = false },
                new FoodItem { Item_Name = "Cottage Pie", Item_Desc = "Our tasty cottage pie packed full of lean minced beef and an assortment of vegetables", Available = true, Vegetarian = false },
                new FoodItem { Item_Name = "Haggis, Neeps and Tatties", Item_Desc = "Scotland national haggis dish.", Available = true, Vegetarian = false },
                new FoodItem { Item_Name = "Bangers and Mash", Item_Desc = "Succulent sausages nestled on a bed of buttery mashed potatoes and drenched in a rich onion gravy", Available = true, Vegetarian = false },
                new FoodItem { Item_Name = "Toad in the Hole", Item_Desc = "Ultimate toad-in-the-hole with caramelised onion gravy", Available = true, Vegetarian = false }
            };

            context.FoodItems.AddRange(foodItems);
            context.SaveChanges();
        }
    }
}
