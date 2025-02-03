using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodOrderSystem_J103042.Data;
using FoodOrderSystem_J103042.Models;

namespace FoodOrderSystem_J103042.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly FoodOrderSystem_J103042Context _context;

        // Single constructor with both logger and database context
        public IndexModel(FoodOrderSystem_J103042Context context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IList<FoodItem> FoodItem { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.FoodItems != null)
            {
                FoodItem = await _context.FoodItems.ToListAsync();
            }
        }
    }
}
