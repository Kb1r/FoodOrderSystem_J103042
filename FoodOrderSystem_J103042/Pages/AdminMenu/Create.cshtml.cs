using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FoodOrderSystem_J103042.Data;
using FoodOrderSystem_J103042.Models;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FoodOrderSystem_J103042.Pages.AdminMenu
{
    public class CreateModel : PageModel
    {
        private readonly FoodOrderSystem_J103042Context _context;

        public CreateModel(FoodOrderSystem_J103042Context context)
        {
            _context = context;
        }

        [BindProperty]
        public FoodItem FoodItem { get; set; } = default!;

        [BindProperty]
        public IFormFile UploadImage { get; set; } = default!;

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

                FoodItem.ImageUrl = "/images/" + fileName;
            }

            _context.FoodItems.Add(FoodItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
