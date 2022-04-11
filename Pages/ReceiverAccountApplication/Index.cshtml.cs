using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.ReceiverAccountApplication
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
            
        }
        public IEnumerable<Receiver> receiverList { get; set; }
        public IEnumerable<SalaryGroup> salaryGroups { get; set; }
        public IEnumerable<ReceiverFamily> receiverFamily { get; set; }
        public IList<Receiver> applicationList { get; set; }=new List<Receiver>();

        public void OnGet()
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
            receiverList =_db.Receiver;
            salaryGroups = _db.SalaryGroup;


            foreach (Receiver receiver in receiverList)
            {
                if(receiver.applicationStatusID<2)
                {
                    applicationList.Add(receiver);                    
                }

            }
            
        }
       
    }
}
