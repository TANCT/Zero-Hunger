using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Controllers
{
    [Route("api/ReceiverAccountApplication")]
    [ApiController]
    public class ReceiverAccountApplicationController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ReceiverAccountApplicationController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            this._hostEnvironment = hostEnvironment;
        }

        public ActionResult Index()
        {
            return View();
        }
        public IList<Receiver> applicationList { get; set; } = new List<Receiver>();
        [HttpGet]
        public IActionResult GetAll()
        {
            foreach (Receiver receiver in _db.Receiver)
            {
                if (receiver.applicationStatusID < 2)
                {
                    receiver.receiverSalaryGroup=_db.SalaryGroup.Where(i => i.salaryGroupID.Equals(receiver.receiverSalaryGroupID)).Single();
                    applicationList.Add(receiver);
                }

            }
            return Json(new { data = applicationList });
        }
    }
}
