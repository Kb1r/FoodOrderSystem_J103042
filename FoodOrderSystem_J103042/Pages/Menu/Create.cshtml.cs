using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FoodOrderSystem_J103042.Models;
using FoodOrderSystem_J103042.Data;

namespace FoodOrderSystem_J103042.Pages.Menu
{
    public class CreateModel : PageModel
    {
        private readonly FoodOrderSystem_J103042.Data.FoodOrderSystem_J103042Context _context;

        public CreateModel(FoodOrderSystem_J103042.Data.FoodOrderSystem_J103042Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public FoodItem FoodItem { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.FoodItems.Add(FoodItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
