using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodOrderSystem.Models;
using FoodOrderSystem_J103042.Data;

namespace FoodOrderSystem_J103042.Pages.Menu
{
    public class DetailsModel : PageModel
    {
        private readonly FoodOrderSystem_J103042.Data.FoodOrderSystem_J103042Context _context;

        public DetailsModel(FoodOrderSystem_J103042.Data.FoodOrderSystem_J103042Context context)
        {
            _context = context;
        }

        public FoodItem FoodItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fooditem = await _context.FoodItems.FirstOrDefaultAsync(m => m.ID == id);
            if (fooditem == null)
            {
                return NotFound();
            }
            else
            {
                FoodItem = fooditem;
            }
            return Page();
        }
    }
}
