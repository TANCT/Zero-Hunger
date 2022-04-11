using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.OData.Edm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages
{
    public class ReceiverQuestionnaireModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public ReceiverQuestionnaireModel(ApplicationDbContext db)
        {
            _db = db;

        }
        [BindProperty]
        public IList<ReceiverFamily> receiverFamilies { get; set; } = new List<ReceiverFamily>();
        [BindProperty]
        public ReceiverQuestionnaire questionnaire { get; set; }
        public Receiver receiver { get; set; }
        [BindProperty]
        public bool property_soHouse { get; set; } = false;
        [BindProperty]
        public bool property_soHouseRent { get; set; } = false;
        [BindProperty]
        public string property_soHouseLoan { get; set; }
        [BindProperty]
        public bool property_rHouse { get; set; } = false;
        [BindProperty]
        public string property_rHouseLoan { get; set; }
        [BindProperty]
        public bool car { get; set; } = false;
        [BindProperty]
        public string carLoan { get; set; }
        [BindProperty]
        public bool motorcycle { get; set; } = false;
        [BindProperty]
        public string motorcycleLoan { get; set; }

        [BindProperty]
        public bool relatives { get; set; } = false;
        [BindProperty]
        public string relativesSponsor { get; set; }
        [BindProperty]
        public bool adopt { get; set; } = false;
        [BindProperty]
        public bool otherSponsorships { get; set; } = false;
        [BindProperty]
        public string otherSponsorshipsSponsor { get; set; }

        [BindProperty]
        public bool partTime { get; set; } = false;
        [BindProperty]
        public bool willingPartTime { get; set; } = false;
        public void OnGet(string id)
        {
            receiver = new Receiver();
            if (id!=null)
            {
                receiver=_db.Receiver.Where(i => i.receiverIC.Equals(id)).Single();

            }
        }

        public async Task<IActionResult> OnPost()
        {
            receiver = new Receiver();
            if(questionnaire!=null)
            {
                receiver = _db.Receiver.Where(i => i.receiverIC.Equals(questionnaire.receiverIC)).Single();
                questionnaire.receiver=receiver;

                questionnaire.otherSponsorship = "";
                if (relatives)
                {
                    questionnaire.otherSponsorship += "Siblings, Friends/Relatives: RM"+relativesSponsor+"/month<br>";
                    if(adopt)
                    {
                        questionnaire.otherSponsorship += "*possible to adopt children<br>";
                    }
                    else
                    {
                        questionnaire.otherSponsorship += "*not possible to adopt children<br>";
                    }
                }
                if(otherSponsorships)
                {
                    questionnaire.otherSponsorship += "Others NGO sponsorship: RM"+otherSponsorshipsSponsor+"/month<br>";
                }

                questionnaire.property = "";
                if(property_soHouse)
                {
                    questionnaire.property += "Self Owned House: RM" + property_soHouseLoan + "/month<br>";
                    if(property_soHouseRent)
                    {
                        questionnaire.property += "*is able to rent out spare room<br>";
                    }
                    else
                    {
                        questionnaire.property += "*not able to rent out spare room<br>";
                    }

                }
                if(property_rHouse)
                {
                    questionnaire.property += "Rented House: RM" + property_rHouseLoan + "/month<br>";
                }
                if(car)
                {
                    questionnaire.property += "Car: RM" + carLoan + "/month<br>";
                }
                if (motorcycle)
                {
                    questionnaire.property += "Motorcycle: RM" + motorcycleLoan + "/month<br>";
                }

                questionnaire.additional = "";
                if(partTime)
                {
                    questionnaire.additional += "15 years old and above children work as part time during weekends/ holiday to support family.<br>";
                }
                else
                {
                    questionnaire.additional += "No 15 years old and above children work as part time during weekends/ holiday to support family.<br>";
                    if (willingPartTime)
                    {
                        questionnaire.additional += "*Willing to let them work part-time<br>";
                    }
                    else
                    {
                        questionnaire.additional += "*Not willing to let them work part-time<br>";
                    }
                }
                
            }

            questionnaire.date = Date.Now.ToString();

            await _db.ReceiverQuestionnaire.AddAsync(questionnaire);

            if(receiver.applicationStatusID == 2)
            {
                receiver.applicationStatusID = 3;
                _db.Receiver.Update(receiver);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToPage("FamilyStatus",new { id= questionnaire.receiverIC });
        }
    }
}
