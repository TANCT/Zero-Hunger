using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;
using System.Text;

namespace ZeroHunger.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        public string emailBody { get; set; } = "";
        public string userEmail { get; set; } = "";
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public User forgotUser { get; set; }
        public ForgotPasswordModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public string resetPasswordLink { get; set; } = "";
        public void OnGet()
        {
           
            
        }
        
        public int p { get; set; }
        public string n { get; set; } = "";
        public string a { get; set; } = "";


        private Random _random = new Random();

        public string RandomString(int size, bool lowerCase = true)
        {
            var builder = new StringBuilder(size);
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
        public void anotherSendEmail(string emailbody, string userEmail)
        {

            var client = new SocketLabsClient(, ""); //Your SocketLabs ServerId and Injection API key

            var message = new BasicMessage();

            message.Subject = "Zero Hunger Reset Password";
            message.HtmlBody = emailbody;
            message.PlainTextBody = emailbody;

            message.From.Email = "";

            //A basic message supports up to 50 recipients and supports several different ways to add recipients
            message.To.Add(userEmail); //Add a To address by passing the email address

            var response = client.Send(message);
        }
        public IActionResult OnPost(string userEmail)
        {
            
                // Find the user by email
                forgotUser = (User)(_db.User.Where(b => b.UserEmail.Equals(userEmail)).FirstOrDefault());
                // If the user is found AND Email is confirmed
                if (forgotUser != null)
                {
                p = (forgotUser.UserID + 777) - 87;
                n = RandomString(20, true);
                a = RandomString(20, true);
                resetPasswordLink = "https://zerohunger20220113131943.azurewebsites.net/ResetPassword?n=" + n+"&p=" +p+"&a="+a;
                string message3 = "<br/><br/>Thanks,<br/>Zero Hunger<br/><b>This a computer auto-generated email, do not reply to this email.</b></p>";
                anotherSendEmail(("Please reset your password <br>" + resetPasswordLink +message3), userEmail);

                    ViewData["Message"] = string.Format("We have sent an email to you.\\Please check your email.");



                    return RedirectToPage("/login");

                }
                
                ViewData["Message"] = string.Format("This email has not been registered yet.\\nPlease register instead.");
                return Page();
            

        }
    }
}
