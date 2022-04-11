using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Controllers
{
    [Route("api/ReceiverList")]
    [ApiController]
    public class ReceiverController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ReceiverController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            this._hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        //public IList<Receiver> receiverList { get; set; } = new List<Receiver>();
        public IList<ReceiverQuestionnaire> questionnaireList { get; set; } = new List<ReceiverQuestionnaire>();
        [HttpGet]
        public IActionResult GetAll()
        {
            foreach (Receiver receiver in _db.Receiver)
            {
                ReceiverQuestionnaire questionnaire=new ReceiverQuestionnaire();
                if (receiver.applicationStatusID >= 2)
                {
                    receiver.receiverSalaryGroup = _db.SalaryGroup.Where(i => i.salaryGroupID.Equals(receiver.receiverSalaryGroupID)).Single();
                    questionnaire = _db.ReceiverQuestionnaire.Where(i => i.receiverIC.Equals(receiver.receiverIC)).OrderBy(j => j.questionnaireId).LastOrDefault();
                    if(questionnaire != null)
                    {
                        questionnaire.receiver=receiver;
                        questionnaireList.Add(questionnaire);
                    }
                    else
                    {
                        questionnaire=new ReceiverQuestionnaire();
                        questionnaire.receiver = receiver;
                        questionnaireList.Add(questionnaire);
                    }
                    
                }

            }
            
            return Json(new { data = questionnaireList });
        }
    }
}
