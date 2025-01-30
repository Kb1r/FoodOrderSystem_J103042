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
    public class IndexModel : PageModel
    {
        private readonly FoodOrderSystem_J103042.Data.FoodOrderSystem_J103042Context _context;

        public IndexModel(FoodOrderSystem_J103042.Data.FoodOrderSystem_J103042Context context)
        {
            _context = context;
        }

        public IList<FoodItem> FoodItems { get; set; } = default!;

        public async Task OnGetAsync()
        {
            FoodItems = await _context.FoodItems.ToListAsync();
        }
    }
}
