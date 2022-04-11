using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.Register
{
    public class RegisterReceiverModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public RegisterReceiverModel(ApplicationDbContext db)
        {
            _db = db;

        }
        [BindProperty]
        public Receiver application { get; set; }
        public IEnumerable<SalaryGroup> salaryGroups { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string street { get; set; }
        public string additional { get; set; }
        public string zip { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string message { get; set; } = "";
        public void anotherSendEmail(string emailbody, string userEmail)
        {

            var client = new SocketLabsClient(, ""); //Your SocketLabs ServerId and Injection API key

            var message = new BasicMessage();

            message.Subject = "We had received your application!";
            message.HtmlBody = emailbody;
            message.PlainTextBody = emailbody;

            message.From.Email = "";

            //A basic message supports up to 50 recipients and supports several different ways to add recipients
            message.To.Add(userEmail); //Add a To address by passing the email address

            var response = client.Send(message);
        }
        public void OnGet()
        {
            salaryGroups = _db.SalaryGroup;
        }
        public async Task<IActionResult> OnPost(Receiver Application, string fName, string lName, string street, string additional, string zip, string city, string state)
        {
            foreach(Receiver receiver in _db.Receiver)
            {
                if(Application.receiverIC==receiver.receiverIC)
                {
                    message = "This IC no. has been registered.";
                    return Page();
                }
            }
            foreach (ReceiverFamily receiverfamily in _db.ReceiverFamily)
            {
                if (Application.receiverIC == receiverfamily.receiverIC)
                {
                    message = "This IC no has been registered.";
                    return Page();
                }
            }
            foreach (Receiver receiver in _db.Receiver)
            {
                if (Application.receiverEmail == receiver.receiverEmail)
                {
                    message = "This email has been registered.";
                    return Page();
                }
            }
            foreach (User user in _db.User)
            {
                if (Application.receiverEmail == user.UserEmail)
                {
                    message = "This email has been registered.";
                    return Page();
                }
            }

            Application.receiverName = fName+" " + lName;
            if(!street.EndsWith(','))
            {
                street += ",";
            }
            if (!additional.EndsWith(','))
            {
                additional += ",";
            }
            Application.receiverAdrs1 = street+" " + additional;
            Application.receiverAdrs2 = zip + " " + city + ", " + state;
            Application.receiverSalaryGroup = _db.SalaryGroup.Where(i => i.salaryGroupID.Equals(Application.receiverSalaryGroupID)).Single();
            Application.healthStatus = "None";
            Application.unhealthyHabit = "None";

            Application.receiverFamilyNo = 0;

            await _db.Receiver.AddAsync(Application);
            await _db.SaveChangesAsync();

           string emailBody = "Thank you for trusting Zero Hunger. "
                    + "We had received your application to become a receiver to us. "
                    + "<br>The below is a summary of your information:<br>"
                    + "Name: " + Application.receiverName
                    + "<br>IC no: " + Application.receiverIC
                    + "<br>Occupation: " + Application.receiverOccupation
                    + "<br>Salary: RM" + Application.receiverSalaryGroup.salaryRange
                    + "<br>Phone No.: " + Application.receiverPhone
                    + "<br>Email: " + Application.receiverEmail
                    + "<br>Address: " + Application.receiverAdrs1 + Application.receiverAdrs2
                    + "<br><br>We will contact you as soon as possible. Please contact us if there is any problem.";

            anotherSendEmail(emailBody, Application.receiverEmail);

           
            return RedirectToPage("RegisterReceiver_Family", Application);
            

        }
    }

}

