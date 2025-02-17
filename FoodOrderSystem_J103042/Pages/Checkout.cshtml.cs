using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace FoodOrderSystem_J103042.Pages
{
    public class CheckoutModel : PageModel
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>
        {
            new CartItem { Name = "Mohinga", Quantity = 2, Price = 5.99m },
            new CartItem { Name = "Laphet Thoke", Quantity = 1, Price = 4.99m }
        };

        public decimal Total => CartItems.Sum(item => item.Quantity * item.Price);

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // TODO: Clear the cart and update the database with the sale history.

            return RedirectToPage("/Index");
        }

        public class CartItem
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }
    }
}