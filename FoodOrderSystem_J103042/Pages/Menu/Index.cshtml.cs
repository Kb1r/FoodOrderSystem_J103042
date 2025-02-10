using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodOrderSystem_J103042.Data;
using FoodOrderSystem_J103042.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodOrderSystem_J103042.Pages.Menu
{
    public class IndexModel : PageModel
    {
        private readonly FoodOrderSystem_J103042Context _context;

        public IndexModel(FoodOrderSystem_J103042Context context)
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
