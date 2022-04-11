using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages
{
    public class editProfileModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public editProfileModel(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            this._hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public User loginUser { get; set; }
        [BindProperty]
        public IFormFile ImageFile { get; set; }
        public void OnGet()
        {
            loginUser = _db.User.Where(b => b.UserEmail.Equals(@User.Identity.Name)).FirstOrDefault();
            

        }
        private string ProcessUploadFile()
        {
            string uniqueFileName = null;
            if (ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "profileImg");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
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
            
            User userFromDB = _db.User.Where(b => b.UserEmail.Equals(@User.Identity.Name)).FirstOrDefault();
            if (ImageFile != null)
            {
                userFromDB.ProfileImage = ProcessUploadFile();
                
            }

            userFromDB.UserName = loginUser.UserName;
            userFromDB.UserEmail = loginUser.UserEmail;
            userFromDB.UserPhone = loginUser.UserPhone;
            userFromDB.UserBirth = loginUser.UserBirth;
            userFromDB.UserAdrs1 = loginUser.UserAdrs1;
            userFromDB.UserAdrs2 = loginUser.UserAdrs2;
            await _db.SaveChangesAsync();
            return RedirectToPage("profile");
        }
    }
}
