using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.CookFood
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<CookedFoodDonation> CFD { get; set; }
        public User u { get; set; }

        public int count { get; set; }
        public int id { get; set; }
        public List<CookedFoodDonation> CFDbyDistance { get; set; }
        public int page {get;set;}
       
        
        public async Task<IActionResult> OnGet()
        {

            string uids=HttpContext.Session.GetString("userid");
            int pp;
            
            string p = Request.Query["page"];
            
            
            int.TryParse(p, out pp);
            page = pp;
            if(p==null)
            {
                page = 0;
            }
            else
            {
                page = pp;
            }
            if(uids==null)
            {
                return RedirectToPage("../login");
            }
            else if (!HttpContext.Request.Cookies["role"].Equals("1"))
             {
                return NotFound();
             }
            int uid;
            int.TryParse(uids, out uid);
            u = await _db.User.FindAsync(uid);
            List<CookReservation> obj =await _db.CookReservation.Where(a => a.userId == uid && a.status.Equals("Pending")).ToListAsync();
            if (obj.Count()==0)
            { 
            CFD = await _db.CookedFoodDonation.ToListAsync();
            count = CFD.Count();
            
            List<CookedFoodDonation> CFDL = new List<CookedFoodDonation>();
            CFDL = CFD.Cast<CookedFoodDonation>().ToList();
           
            CFDL.RemoveAll(x => x.RemainQuantity <= 0);
            CFDL.RemoveAll(x => x.CloseDate <= DateTime.Now);
                if(u.latitude==null)
                {
                    u.latitude = 0;
                }
                if(u.longitute==null)
                {
                    u.longitute = 0;
                }
            foreach(var item in CFDL)
                {
                    
                    item.distance = GetDistance(item.CookLatitude, item.CookLongtitude, (float)u.latitude, (float)u.longitute);
                }
                CFDL.Sort((x,y)=>x.distance.CompareTo(y.distance));
            CFD = (IEnumerable<CookedFoodDonation>)CFDL;
            
                return Page();
            }
            else
            {
                return RedirectToPage("Reservation");
            }
        }

        public async Task OnPost(int ID)
        {
            string uids = HttpContext.Session.GetString("userid");
            int uid;
            int.TryParse(uids, out uid);
            u = await _db.User.FindAsync(uid);
            CookReservation crs = new CookReservation();
            var cfd = await _db.CookedFoodDonation.FindAsync(ID);
            if (cfd.Reservation < cfd.RemainQuantity)
            {
                cfd.Reservation = cfd.Reservation + 1;
                crs.date = System.DateTime.Now.ToString("g");
                crs.reservationRefUser = (_db.User.Where(b => b.UserEmail.Equals(@User.Identity.Name)).FirstOrDefault());
                crs.status = "Pending";
                crs.userId = crs.reservationRefUser.UserID;
                crs.reservationRefCook = (await _db.CookedFoodDonation.FindAsync(ID));
                crs.cookId = crs.reservationRefCook.CookID;
                await _db.CookReservation.AddAsync(crs);
                await _db.SaveChangesAsync();
                var reseruser = await _db.User.FindAsync(crs.userId);
                var cookuser = await _db.User.FindAsync(cfd.DonorUserID);

                string message = "<p style='font-size:large;'> <b style='font-size:x-large;'>Dear " + cookuser.UserName + "</b>,<br/>Your cooked food donation <b>" + cfd.CookName + "</b> which open on " + cfd.OpenDate + " has been <b style='color:green;'>RESERVED</b> by the following receiver:<br/>";
                string message2= "<b>Name:</b> " + reseruser.UserName+ "<br/><b>Phone:</b> " + reseruser.UserPhone+ "<br/><b>Time:</b> " + crs.date+ "<br/><br/><br/><b>" + cfd.CookName+" Remain Quantity:</b>  "+(cfd.RemainQuantity-cfd.Reservation);
                string message3 = "<br/><br/>Thanks,<br/>Zero Hunger<br/><b>This a computer auto-generated email, do not reply to this email.</b></p>";
                anotherSendEmail(message+message2+message3,cookuser.UserEmail);
                CFD = await _db.CookedFoodDonation.ToListAsync();
                Response.Redirect("CookFood");
            }
            else
            {
                Response.Redirect("CookFood");
            }
            
        }
        public async Task<IActionResult> OnPostUpdate(string userLat,string userLon,string handler)
        {
            string userlat = userLat;
            string userlon = userLon;
            string uids = HttpContext.Session.GetString("userid");
            int uid;
            int.TryParse(uids, out uid);
             u = await _db.User.FindAsync(uid);
            float userlatitude;
            float userlongitute;
            float.TryParse(userlat, out userlatitude);
            float.TryParse(userlon, out userlongitute);
            u.latitude = userlatitude;
            u.longitute = userlongitute;
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
        public float GetDistance(float cla,float clo,float ula,float ulo)
        {
            
            float radlat1;
            float radlat2;
            float theta;
            float radtheta;
            float Cdistance;
            radlat1 = (float)(cla * Math.PI  / 180);
            radlat2 = (float)(ula * Math.PI / 180);
            theta = (float)(clo - ulo);
            radtheta = (float)(theta * Math.PI / 180);
            Cdistance = (float)Math.Sin(radlat1) * (float)Math.Sin(radlat2) + (float)Math.Cos(radlat1) * (float)Math.Cos(radlat2) * (float)Math.Cos(radtheta);
            Cdistance = (float)Math.Acos(Cdistance);
            Cdistance = (float)(Cdistance * 180 / Math.PI);//convert to angle
            Cdistance = (float)(Cdistance * 60 * 1.1515);
            Cdistance = (float)(Cdistance * 1.609344);
            decimal d = (decimal)Cdistance;
            d=Decimal.Round(d, 2);
            Cdistance = (float)d;
            return Cdistance;
        }
        public void anotherSendEmail(string emailbody,string userEmail)
        {
            
            var client = new SocketLabsClient(ServerId, "API key"); //Your SocketLabs ServerId and Injection API key

            var message = new BasicMessage();

            message.Subject = "Reservation received";
            message.HtmlBody = emailbody;
            message.PlainTextBody = emailbody;

            message.From.Email = "Your email";//Your email

            //A basic message supports up to 50 recipients and supports several different ways to add recipients
            message.To.Add(userEmail); //Add a To address by passing the email address

            var response = client.Send(message);
        }

    }
}
