using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.ReceiverAccountApplication
{
    public class RejectReceiverAccountApplicationModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public RejectReceiverAccountApplicationModel(ApplicationDbContext db)
        {
            _db = db;

        }
        public Receiver application { get; set; }
        public string message { get; set; } = "";

        //public static void SendEmail(string emailbody, string userEmail)
        //{
        //    // Specify the from and to email address
        //    MailMessage mailMessage = new MailMessage("vtechzerohunger@gmail.com", userEmail);
        //    // Specify the email body
        //    mailMessage.Body = emailbody;
        //    // Specify the email Subject
        //    mailMessage.Subject = "Sorry! Your application is rejected!";

        //    // Specify the SMTP server name and post number
        //    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
        //    // Specify your gmail address and password
        //    smtpClient.Credentials = new System.Net.NetworkCredential()
        //    {
        //        UserName = "vtechzerohunger@gmail.com",
        //        Password = "ad_0hunger"
        //    };
        //    // Gmail works on SSL, so set this property to true
        //    smtpClient.EnableSsl = true;
        //    // Finall send the email message using Send() method
        //    smtpClient.Send(mailMessage);
        //}
        public void anotherSendEmail(string emailbody, string userEmail)
        {

            var client = new SocketLabsClient(42290, "e5N6HwBb2k8A3Mrc9R7K"); //Your SocketLabs ServerId and Injection API key

            var message = new BasicMessage();

            message.Subject = "Sorry! Your application is rejected!";
            message.HtmlBody = emailbody;
            message.PlainTextBody = emailbody;

            message.From.Email = "vtechzerohunger@gmail.com";

            //A basic message supports up to 50 recipients and supports several different ways to add recipients
            message.To.Add(userEmail); //Add a To address by passing the email address

            var response = client.Send(message);
        }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            string uids = HttpContext.Session.GetString("userid");
            if (uids == null)
            {
                return RedirectToPage("../login");
            }
            else
            {
                if (HttpContext.Request.Cookies["role"] != null && !HttpContext.Request.Cookies["role"].Equals("4"))
                {

                    return RedirectToPage("../index");
                }

            }
            application = new Receiver();
            if (id != null)
            {
                application = _db.Receiver.Where(i => i.receiverIC.Equals(id)).Single();
                foreach (ReceiverFamily familyInfo in _db.ReceiverFamily)
                {
                    if (familyInfo.receiverIC.Equals(id))
                    {
                        _db.ReceiverFamily.Remove(familyInfo);
                    }
                }
                string email = application.receiverEmail;

                _db.Receiver.Remove(application);

                await _db.SaveChangesAsync();

                string emailBody = "We appreciate you taking the time to apply as a receiver to ZeroHunger. "
                    + "However, in order to make the best use of the resources available to us, we had to make the painful decision to refuse your application.";

                anotherSendEmail(emailBody, email);

                message = "The application is removed. An email is sent to the applicant's e-mail address.";
                TempData["alertMessage"] = "The application has been rejected. An email is sent to the applicant." +
                    "email: " + application.receiverEmail;
                return RedirectToPage("Index");
            }
            message = "Error!";
            return RedirectToPage("ViewReceiverAccountApplication");

        }
    }
}
