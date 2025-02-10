using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using FoodOrderSystem_J103042.Data;
using FoodOrderSystem_J103042.Models;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace FoodOrderSystem_J103042.Pages.AdminMenu
{
    public class EditModel : PageModel
    {
        private readonly FoodOrderSystem_J103042Context _context;

        public EditModel(FoodOrderSystem_J103042Context context)
        {
            _context = context;
        }

        [BindProperty]
        public FoodItem FoodItem { get; set; } = default!;

      
        [BindProperty]
        public IFormFile? UploadImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            FoodItem = await _context.FoodItems.FindAsync(id);

            if (FoodItem == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

           
            if (UploadImage != null)
            {
                var fileName = Path.GetFileName(UploadImage.FileName);
                var filePath = Path.Combine("wwwroot/images", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await UploadImage.CopyToAsync(fileStream);
                }

          
                FoodItem.ImageUrl = fileName;
            }

            _context.Attach(FoodItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.FoodItems.Any(e => e.ID == FoodItem.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
