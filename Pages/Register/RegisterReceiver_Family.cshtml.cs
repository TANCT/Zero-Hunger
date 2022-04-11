using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.Register
{
    public class RegisterReceiver_FamilyModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public RegisterReceiver_FamilyModel(ApplicationDbContext db)
        {
            _db = db;

        }
        [BindProperty]
        public ReceiverFamily applicationFamily { get; set; }
        public IList<ReceiverFamily> applicationFamilies { get; set; }=new List<ReceiverFamily>();
        public IEnumerable<SalaryGroup> salaryGroups { get; set; }
        public Receiver application { get; set; }
        public int noFamily { get; set; }
        public string message { get; set; } = "";
        public void OnGet(Receiver Application)
        {
            
            salaryGroups = _db.SalaryGroup;
            if (Application!=null)
            {
                application = Application;
                foreach (ReceiverFamily familyInfo in _db.ReceiverFamily)
                {
                    if (familyInfo.receiverIC.Equals(Application.receiverIC))
                    {
                        applicationFamilies.Add(familyInfo);
                    }
                }
                noFamily = application.receiverFamilyNo;
            }

        }
        public async Task<IActionResult> OnPostAsync(ReceiverFamily ApplicationFamily)
        {
            
            foreach (Receiver receiver in _db.Receiver)
            {
                if (ApplicationFamily.familyIC == receiver.receiverIC)
                {
                    message = "This IC no. has been registered.";
                    return Page();
                }
            }
            foreach (ReceiverFamily receiverfamily in _db.ReceiverFamily)
            {
                if (ApplicationFamily.familyIC == receiverfamily.receiverIC)
                {
                    message = "This IC no. has been registered.";
                    return Page();
                }
            }
            ApplicationFamily.familySalaryGroup = _db.SalaryGroup.Where(i => i.salaryGroupID.Equals(ApplicationFamily.familySalaryGroupID)).Single();

            ApplicationFamily.healthStatus = "None";
            ApplicationFamily.unhealthyHabit = "None";
            
            application= _db.Receiver.Where(i => i.receiverIC.Equals(ApplicationFamily.receiverIC)).Single();
            ApplicationFamily.receiver = _db.Receiver.Where(i => i.receiverIC.Equals(ApplicationFamily.receiverIC)).Single();
            application.receiverFamilyNo+=1;
            _db.Receiver.Update(application);
            await _db.ReceiverFamily.AddAsync(ApplicationFamily);
            await _db.SaveChangesAsync();
            return RedirectToPage("RegisterReceiver_Family", application);

        }
    }
}
