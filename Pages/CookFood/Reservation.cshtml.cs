using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ZeroHunger.Pages.CookFood
{

    public class ReservationModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public ReservationModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<CookedFoodDonation> CFD { get; set; }
        public IEnumerable<CookReservation> CR { get; set; }
        public IEnumerable<CookReservation> CRP { get; set; }
        public User donor { get; set; }


        public async Task<IActionResult> OnGet()
        {
            int uid;
            int.TryParse(HttpContext.Session.GetString("userid"), out uid);
            string uids = HttpContext.Session.GetString("userid");
            if (uids == null)
            {
                return RedirectToPage("../login");
            }
            else
            {
                if (!HttpContext.Request.Cookies["role"].Equals("1"))
                {

                    return NotFound();
                }

            }
            CR = await _db.CookReservation.Where(b => b.userId==uid).ToListAsync();
            foreach (var item in CR)
            {
                item.reservationRefCook = await _db.CookedFoodDonation.FindAsync(item.cookId);
            }
            if (CR.Count() != 0)
            {
                donor = await _db.User.FindAsync(CR.ElementAt(0).reservationRefCook.DonorUserID);
            }
            CRP = CR.Where(c => c.status.Equals("Pending"));

            CR = CR.Where(c => c.status.Equals("Success") || c.status.Equals("Expired")||c.status.Equals("Confirmed"));
            return Page();

        }
        public async Task<IActionResult> OnPostComplete(int id)
        {
            var CookReservation = await _db.CookReservation.FindAsync(id);
            if (CookReservation == null)
            {
                return NotFound();
            }
            CookReservation.status = "Success";
            await _db.SaveChangesAsync();
            return RedirectToPage("Reservation");
        }
        public async Task<IActionResult> OnPostCancel(int id)
        {
            CookReservation Cookreservation = await _db.CookReservation.FindAsync(id);
            if (Cookreservation == null)
            {
                return NotFound();
            }
            var cfd = await _db.CookedFoodDonation.FindAsync(Cookreservation.cookId);
            cfd.Reservation = cfd.Reservation - 1;
            _db.CookReservation.Remove(Cookreservation);
            await _db.SaveChangesAsync();
            var reseruser = await _db.User.FindAsync(Cookreservation.userId);
            var cookuser = await _db.User.FindAsync(cfd.DonorUserID);
            string message = "<p style='font-size:large;'> <b style='font-size:x-large;'>Dear " + cookuser.UserName + "</b>,<br/>Your cooked food donation <b>" + cfd.CookName + "</b> which open on " + cfd.OpenDate + " has been <b style='color:red'>CANCELED</b> by the following receiver:<br/>";
            string message2 = "<b>Name:</b> " + reseruser.UserName + "<br/><b>Phone:</b> " + reseruser.UserPhone + "<br/><b>Time:</b> " + Cookreservation.date + "<br/><b>" + cfd.CookName + " Remain Quantity:</b>  " + cfd.RemainQuantity;
            string message3 = "<br/><br/>Thanks,<br/>Zero Hunger<br/><b>This a computer auto-generated email, do not reply to this email.</b></p>";
            anotherSendEmail(message + message2 + message3, cookuser.UserEmail);
            return RedirectToPage("Reservation");
        }
        public void anotherSendEmail(string emailbody, string userEmail)
        {
            
            var client = new SocketLabsClient(ServerID, "API Key"); //Your SocketLabs ServerId and Injection API key

            var message = new BasicMessage();

            message.Subject = "Reservation Canceled";
            message.HtmlBody = emailbody;
            message.PlainTextBody = emailbody;

            message.From.Email = "Your Email";//Your Email

            //A basic message supports up to 50 recipients and supports several different ways to add recipients
            message.To.Add(userEmail); //Add a To address by passing the email address

            var response = client.Send(message);
        }


    }
}
