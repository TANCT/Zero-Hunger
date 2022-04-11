using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.DryFoodDonationF
{
    
    public class AddDryFoodDonationModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public AddDryFoodDonationModel(ApplicationDbContext db)
        {
            _db=db;
        }
        [BindProperty]
        public DryFoodDonation DFD { get; set; }
        public IEnumerable<User> Admin { get; set; }
        public async Task<IActionResult> OnGet()
        {
            string uids = HttpContext.Session.GetString("userid");
            if (uids == null)
            {
                return RedirectToPage("../login");
            }
            else
            {
                if(HttpContext.Request.Cookies["role"].Equals("3"))
                {
                    return Page();
                }
                else
                {
                    return NotFound();
                }
                
            }
        }
        public async Task<IActionResult> OnPost(DryFoodDonation dfd)
        {
            string uids = HttpContext.Session.GetString("userid");
            int uid;
            int.TryParse(uids, out uid);

            if (ModelState.IsValid)
            {
                dfd.donorid = uid;
                dfd.DryFoodRemainQuantity = dfd.DryFoodQuantity;
                dfd.donor_Id = _db.User.Find(dfd.donorid);
                await _db.DryFoodDonation.AddAsync(dfd);
                await _db.SaveChangesAsync();
                Admin = await _db.User.Where(b => b.TypeId==4).ToListAsync();
                string message = "<p style='font-size:20px;'> <b style='font-size:x-large;'> Dry Food Donation Received</b><br/>";
                string dn = "<b>Name:</b> " + dfd.donor_Id.UserName + "<br/>";
                string dp = "<b>Phone:</b> " + dfd.donor_Id.UserPhone + "<br/>";
                string item = "<b>Item:</b> " + dfd.DryFoodName + "<br/>";
                string q = "<b>Quantity:</b>  " + dfd.DryFoodQuantity + "<br/>";
                string dm = "<b>Delivery Method:</b> <b style='color:red'>" + dfd.DeliveryMethod + "</b><br/>";
                string rm = "<b>Remark:</b>  " + dfd.DryFoodRemark + "<br/>";
                string pd = "<b>Pick up date:</b><b style='color:red'> " + dfd.DryFoodPickDate + "</b><br/>";
                string footer = "<br/><br/>Thanks,<br/>Zero Hunger<br/><b>This a computer auto-generated email, do not reply to this email.</b></p>";
                foreach(var adminitem in Admin)
                {
                    anotherSendEmail((message + dn + dp + item + pd + dm + q  + rm + footer),adminitem.UserEmail);
                }
                
                return RedirectToPage("DryFoodIndex");
                
            }
            else
            {
                return Page();
            }
        }
        public void anotherSendEmail(string emailbody, string userEmail)
        {
            
            var client = new SocketLabsClient(Serverid, "API KEY"); //Your SocketLabs ServerId and Injection API key

            var message = new BasicMessage();

            message.Subject = "Donation Received";
            message.HtmlBody = emailbody;
            message.PlainTextBody = emailbody;

            message.From.Email = "your email";//Your Email

            //A basic message supports up to 50 recipients and supports several different ways to add recipients
            message.To.Add(userEmail); //Add a To address by passing the email address

            var response = client.Send(message);
        }
    }
}
