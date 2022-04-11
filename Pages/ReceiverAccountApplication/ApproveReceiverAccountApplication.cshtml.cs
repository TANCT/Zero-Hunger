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

namespace ZeroHunger.Pages
{
    public class ApproveReceiverAccountApplicationModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public ApproveReceiverAccountApplicationModel(ApplicationDbContext db)
        {
            _db = db;

        }
        public Receiver application { get; set; }
        public User user { get; set; }
        public string message { get; set; } = "";
        public string emailBody { get; set; } = "";

        //public static void SendEmail(string emailbody, string userEmail)
        //{
        //    // Specify the from and to email address
        //    MailMessage mailMessage = new MailMessage("vtechzerohunger@gmail.com", userEmail);
        //    // Specify the email body
        //    mailMessage.Body = emailbody;
        //    // Specify the email Subject
        //    mailMessage.Subject = "Your application is approved!";

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

            message.Subject = "Your application is approved!";
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
            user= new User();
            if(id!=null)
            {
                application = _db.Receiver.Where(i => i.receiverIC.Equals(id)).Single();
                application.applicationStatusID = 2;

                user.UserType = (UserType)(_db.UserType.Where(b => b.TypeID.Equals(1)).FirstOrDefault());
                user.UserName = application.receiverName;
                user.UserPwd = "123";
                user.UserEmail=application.receiverEmail;
                int year = int.Parse(application.receiverDOB.Substring(0, 4));
                int month = int.Parse(application.receiverDOB.Substring(5, 2));
                int day = int.Parse(application.receiverDOB.Substring(8, 2));
                user.UserBirth = new System.DateTime(year,month,day);
                user.UserAdrs1 = application.receiverAdrs1;
                user.UserAdrs2=application.receiverAdrs2;
                user.UserPhone = application.receiverPhone;

                await _db.User.AddAsync(user);
                await _db.SaveChangesAsync();

                application.user=new User();
                application.user=_db.User.Where(i=>i.UserEmail.Equals(application.receiverEmail)).Single();
                application.userID = application.user.UserID;
                _db.Receiver.Update(application);

                await _db.SaveChangesAsync();

                emailBody = "We appreciate you taking the time to apply as a receiver to ZeroHunger. "
                    + "We are pleased to tell you that your application is approved.<br>"
                    + "Please login to your account with the following information and change the password:<br>"
                    + "Email: " + application.receiverEmail
                    + "<br>Password: 123"
                    + "<br>https://zerohunger20220113131943.azurewebsites.net/login";

                anotherSendEmail(emailBody, application.receiverEmail);

                message = "The application is approved. An email is sent to the applicant's e-mail address. " +
                    "email: " + application.receiverEmail + " " +
                    "password: " + "123";
                TempData["alertMessage"] = "The application is approved. An email is sent to the applicant." +
                    "email: " + application.receiverEmail + " " +
                    "password: " + "123";
                return RedirectToPage("Index");
            }
            message = "Error!";
            return RedirectToPage("ViewReceiverAccountApplication");

        }
    }
}
