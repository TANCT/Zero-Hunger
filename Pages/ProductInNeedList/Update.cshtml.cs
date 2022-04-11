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
    public class UpdateModel : PageModel
    {
        private ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public UpdateModel(ApplicationDbContext db,IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            this._hostEnvironment = hostEnvironment;

        }
        [BindProperty]
        public ProductInNeed product { get; set; }
        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public async Task OnGet(int id) => product = await _db.ProductInNeed.FindAsync(id);

        private string ProcessUploadFile()
        {
            string uniqueFileName = null;
            if (ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                string filename = ImageFile.FileName;
                filename=filename.Replace(" ", String.Empty);
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
            

            if (ModelState.IsValid)
            {

                var productFromDB = await _db.ProductInNeed.FindAsync(product.product_id);

                productFromDB.product_name = product.product_name;
                if (product.product_description != null)
                    productFromDB.product_description = product.product_description;

                productFromDB.amount = product.amount;
                

                if (ImageFile != null)
                {
                    if (productFromDB.image != null)
                    {
                        string filepath = Path.Combine(_hostEnvironment.WebRootPath, "images",productFromDB.image );
                        System.IO.File.Delete(filepath);
                    }
                    productFromDB.image = ProcessUploadFile();

                }

                await _db.SaveChangesAsync();
                return RedirectToPage("Index");

            }
            return RedirectToPage();
        }
    }
}
