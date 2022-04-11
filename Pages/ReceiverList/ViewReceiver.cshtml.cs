using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.ReceiverList
{
    public class ViewReceiverModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public ViewReceiverModel(ApplicationDbContext db)
        {
            _db = db;

        }
        public void anotherSendEmail(string emailbody, string userEmail,string action)
        {

            var client = new SocketLabsClient(42290, "e5N6HwBb2k8A3Mrc9R7K"); //Your SocketLabs ServerId and Injection API key

            var message = new BasicMessage();

            if(action.Equals("email"))
            {
                message.Subject = "Please update your information";
            }
            else if(action.Equals("delete"))
            {
                message.Subject = "Sorry! Your receiver account is removed!";
            }

            
            message.HtmlBody = emailbody;
            message.PlainTextBody = emailbody;

            message.From.Email = "vtechzerohunger@gmail.com";

            //A basic message supports up to 50 recipients and supports several different ways to add recipients
            message.To.Add(userEmail); //Add a To address by passing the email address

            var response = client.Send(message);
        }
        public Receiver receiver { get; set; }
        public IList<ReceiverFamily> family { get; set; } = new List<ReceiverFamily>();
        public IEnumerable<SalaryGroup> salaryGroups { get; set; }
        public ReceiverQuestionnaire questionnaire { get; set; }
        public async Task OnGetAsync(string? id,string? action)
        {
            string uids = HttpContext.Session.GetString("userid");
            if (uids == null)
            {
                Response.Redirect("../login");
            }
            else
            {
                if (HttpContext.Request.Cookies["role"] != null && !HttpContext.Request.Cookies["role"].Equals("4"))
                {

                    Response.Redirect("../index");
                }

            }
            receiver = new Receiver();
            questionnaire = new ReceiverQuestionnaire();
            salaryGroups = _db.SalaryGroup;

            if (id != null)
            {
                receiver = _db.Receiver.Where(i => i.receiverIC.Equals(id)).Single();
                questionnaire = _db.ReceiverQuestionnaire.Where(i => i.receiverIC.Equals(id)).OrderBy(j => j.questionnaireId).LastOrDefault();

                    foreach (ReceiverFamily familyInfo in _db.ReceiverFamily)
                    {
                        if (familyInfo.receiverIC.Equals(id))
                        {
                            family.Add(familyInfo);
                        }
                    }

                    if (receiver.applicationStatusID == 3)
                    {
                        receiver.applicationStatusID = 2;
                        _db.Receiver.Update(receiver);

                    }
                    

                if(action != null)
                {
                    if (action.Equals("email"))
                    {
                        string emailBody = "Please spend some time to update your information through this questionnaire through the link below. "
                        + "<br>https://zerohunger20220113131943.azurewebsites.net/ReceiverQuestionnaire?id=" + receiver.receiverIC
                        +"<br><br>Thanks for the cooperation.";

                        anotherSendEmail(emailBody, receiver.receiverEmail,action);

                        TempData["alertMessage"] = "The questionnaire is emailed to the receiver.";

                    }
                    else if(action.Equals("delete"))
                    {
                        _db.ReceiverFamily.RemoveRange(family);
                        List<ReceiverQuestionnaire> questionnaires=_db.ReceiverQuestionnaire.Where(i=>i.receiverIC.Equals(receiver.receiverIC)).ToList();
                        _db.ReceiverQuestionnaire.RemoveRange(questionnaires);
                        _db.User.Remove(_db.User.Where(i=>i.UserID.Equals(receiver.userID)).FirstOrDefault());
                        _db.Receiver.Remove(receiver);
                        await _db.SaveChangesAsync();
                        TempData["alertMessage"] = "The receiver is removed.";

                        string emailBody = "We appreciate you taking the time to apply as a receiver to ZeroHunger. "
                    + "However, in order to make the best use of the resources available to us, we had to make the painful decision to remove your account as a receiver. " +
                    " If you have needs, please don't hesitate to register again to give us a chance to reconsider it.";

                        anotherSendEmail(emailBody, receiver.receiverEmail,action);

                        //return RedirectToPage("Index");
                        Response.Redirect("Index");
                    }
                }

                await _db.SaveChangesAsync();

            }
            else
            {
                TempData["alertMessage"] = "Error!";
                //return RedirectToPage("Index");
                Response.Redirect("Index");
            }
            
        }
    }
}
