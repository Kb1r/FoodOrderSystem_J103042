using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using FoodOrderSystem_J103042.Models;

namespace FoodOrderSystem_J103042.Pages.Menu
{
    public class IndexModel : PageModel
    {
        private readonly FoodOrderSystem_J103042.Data.FoodOrderSystem_J103042Context _context;

        public IndexModel(FoodOrderSystem_J103042.Data.FoodOrderSystem_J103042Context context)
        {
            _context = context;
        }

        public List<FoodItem> FoodItems { get; set; } = new List<FoodItem>();

        public void OnGet()
        {
            FoodItems = _context.FoodItems.ToList();
        }

        public IActionResult OnPostAddToCart(int itemId, string name, decimal price)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            var existingItem = cart.FirstOrDefault(i => i.Id == itemId);
            if (existingItem != null)
            {
                existingItem.Quantity++; // Increase quantity if item exists
            }
            else
            {
                cart.Add(new CartItem { Id = itemId, Name = name, Price = price, Quantity = 1 });
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToPage();
        }
    }

    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
