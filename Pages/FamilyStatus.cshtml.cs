using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages
{
    public class FamilyStatusModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public FamilyStatusModel(ApplicationDbContext db)
        {
            _db = db;

        }
        [BindProperty]
        public IList<ReceiverFamily> receiverFamilies { get; set; } = new List<ReceiverFamily>();
        [BindProperty]
        public Receiver receiver { get; set; }
        public IEnumerable<SalaryGroup> salaryGroups { get; set; }
        [BindProperty]
        public string rDuration { get; set; } = "";
        [BindProperty]
        public bool rInsurance { get; set; } = false;
        [BindProperty]
        public string rhospital { get; set; } = "";
        [BindProperty]
        public string rMCost { get; set; } = "";
        [BindProperty]
        public bool rSmoking { get; set; } = false;
        [BindProperty]
        public bool rDrinking { get; set; } = false;
        [BindProperty]
        public bool rGambling { get; set; } = false;

        [BindProperty]
        public List<string> fDuration { get; set; } = new List<string>();
        [BindProperty]
        public List<bool> fInsurance { get; set; } = new List<bool>();
        [BindProperty]
        public List<string> fCost { get; set; } = new List<string>();
        [BindProperty]
        public List<string> fhospital { get; set; } = new List<string>();
        [BindProperty]
        public List<bool> fSmoking { get; set; } = new List<bool>();
        [BindProperty]
        public List<bool> fDrinking { get; set; } = new List<bool>();
        [BindProperty]
        public List<bool> fGambling { get; set; } = new List<bool>();
        public void OnGet(string id)
        {
            receiver = new Receiver();
            if (id != null)
            {

                receiver = _db.Receiver.Where(i => i.receiverIC.Equals(id)).Single();
                salaryGroups = _db.SalaryGroup;

                foreach (ReceiverFamily familyInfo in _db.ReceiverFamily)
                {
                    if (familyInfo.receiverIC.Equals(id))
                    {
                        receiverFamilies.Add(familyInfo);
                        fDuration.Add("");
                        fInsurance.Add(false);
                        fCost.Add("");
                        fhospital.Add("");
                        fSmoking.Add(false);
                        fDrinking.Add(false);
                        fGambling.Add(false);
                    }
                }
            }
        }

        public async Task<IActionResult> OnPost()
        {
            Receiver receiverToUpdate = _db.Receiver.Where(i => i.receiverIC.Equals(receiver.receiverIC)).Single();
            receiverToUpdate.receiverSalaryGroupID = receiver.receiverSalaryGroupID;
            receiverToUpdate.receiverSalaryGroup = _db.SalaryGroup.Where(i => i.salaryGroupID.Equals(receiverToUpdate.receiverSalaryGroupID)).Single();
            receiverToUpdate.receiverOccupation = receiver.receiverOccupation;
            receiverToUpdate.receiverCompany = receiver.receiverCompany;

            receiverToUpdate.healthStatus = receiver.healthStatus;
            if (receiverToUpdate.healthStatus.Equals("Sick"))
            {
                receiverToUpdate.healthStatus += "<br>Duration: " + rDuration;
                if (rInsurance)
                {
                    receiverToUpdate.healthStatus += "<br>*has Insurance/SOCSO";
                }
                receiverToUpdate.healthStatus += "<br>Hospital: " + rhospital;
                receiverToUpdate.healthStatus += "<br>Medical Cost: RM" + rMCost;
            }
            
            receiverToUpdate.unhealthyHabit = "";
            if (rSmoking)
            {
                receiverToUpdate.unhealthyHabit += "Smoking<br>";
            }
            if (rDrinking)
            {
                receiverToUpdate.unhealthyHabit += "Drinking<br>";
            }
            if (rGambling)
            {
                receiverToUpdate.unhealthyHabit += "Gambling<br>";
            }
            if (receiverToUpdate.unhealthyHabit.Equals(""))
            {
                receiverToUpdate.unhealthyHabit = "None";
            }

            _db.Receiver.Update(receiverToUpdate);

            ReceiverFamily familyToUpdate;
            for (int i = 0; i < receiverFamilies.Count(); i++)
            {
                familyToUpdate = _db.ReceiverFamily.Where(j => j.familyIC.Equals(receiverFamilies[i].familyIC)).Single();
                familyToUpdate.familyOccupation = receiverFamilies[i].familyOccupation;
                familyToUpdate.familyCompanyOrSchool = receiverFamilies[i].familyCompanyOrSchool;
                familyToUpdate.familySalaryGroupID = receiverFamilies[i].familySalaryGroupID;
                familyToUpdate.familySalaryGroup = _db.SalaryGroup.Where(i => i.salaryGroupID.Equals(familyToUpdate.familySalaryGroupID)).Single();

                familyToUpdate.healthStatus = receiverFamilies[i].healthStatus;
                if (familyToUpdate.healthStatus.Equals("Sick"))
                {
                    familyToUpdate.healthStatus += "<br>Duration: " + fDuration[i];
                    if (fInsurance[i])
                    {
                        familyToUpdate.healthStatus += "<br>*has Insurance/SOCSO";
                    }
                    familyToUpdate.healthStatus += "<br>Hospital: " + fhospital[i];
                    familyToUpdate.healthStatus += "<br>Medical Cost: RM" + fCost[i];
                }
                familyToUpdate.unhealthyHabit = "";
                if (fSmoking[i])
                {
                    familyToUpdate.unhealthyHabit += "Smoking<br>";
                }
                if (fDrinking[i])
                {
                    familyToUpdate.unhealthyHabit += "Drinking<br>";
                }
                if (fGambling[i])
                {
                    familyToUpdate.unhealthyHabit += "Gambling<br>";
                }
                if (familyToUpdate.unhealthyHabit.Equals(""))
                {
                    familyToUpdate.unhealthyHabit = "None";
                }

                _db.ReceiverFamily.Update(familyToUpdate);


            }
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
