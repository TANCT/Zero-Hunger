using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.ProductInNeedList
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CreateModel(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            this._hostEnvironment = hostEnvironment;
        }
        [BindProperty]
        public ProductInNeed product { get; set; }

        [BindProperty]
        public IFormFile ImageFile { get; set; }
        public void OnGet()
        {

        }
        private string ProcessUploadFile()
        {
            string uniqueFileName = null;
            if (ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                String filename = ImageFile.FileName;
                filename = filename.Replace(" ", String.Empty);
                filename = filename.Replace("\'", String.Empty);
                filename = filename.Replace("\"", String.Empty);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(filename);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(fileStream);

                }


            }
            return uniqueFileName;

        }
        public async Task<IActionResult> OnPost()
        {
            if (ImageFile != null)
            {
                product.image=ProcessUploadFile();

            }
            
            
            if (ModelState.IsValid)
            {

                await _db.ProductInNeed.AddAsync(product);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");

            }
            else
            {
                return Page();
            }
        }
    }
}
