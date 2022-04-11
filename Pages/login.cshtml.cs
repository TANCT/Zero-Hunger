using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Threading.Tasks;
using ZeroHunger.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using ZeroHunger.Data;

namespace ZeroHunger.Pages
{
    public class loginModel : PageModel
    {
        private ApplicationDbContext _db;

        public loginModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public User user { get; set; }
        //public async Task OnGet(int userID)
        //{
        //    user = await _db.User.FindAsync(userID);

        //}
        public IEnumerable<User> Users { get; set; }
        public bool remember { get; set; } = true;
        public void OnGet()
        {
            
        }


        public async Task<IActionResult> OnPostAsync()
        {
            //string connString = "Server=sql.bsite.net\\MSSQL2016;Database=zerohunger_;User id=zerohunger_;Password=ad_0hunger;Trusted_Connection=False;MultipleActiveResultSets=true";
            //// int count = 0;
            //SqlConnection sqlConn = new SqlConnection(connString);
            //using (SqlConnection myConnection = new SqlConnection(connString))
            //{
                //myConnection.Open();
                //string oString = "SELECT * FROM [User] WHERE UserEmail=@email AND UserPwd=@password";
                //SqlCommand oCmd = new SqlCommand(oString, myConnection);
                //SqlDataReader sdr = oCmd.ExecuteReader();
                //if (sdr.Read())
                //{
                //string retrieveName = "SELECT UserName FROM [User] WHERE UserEmail=@email AND UserPwd=@password";
                var obj = _db.User.Where(a => a.UserEmail.Equals(user.UserEmail)).FirstOrDefault();
                //oCmd.Parameters.AddWithValue("@email", user.UserEmail.Trim());
                //oCmd.Parameters.AddWithValue("@password", user.UserPwd.Trim());
                //SqlDataAdapter sda = new SqlDataAdapter(oCmd);
                //DataTable dt = new DataTable();
                //sda.Fill(dt);
                //int i = oCmd.ExecuteNonQuery();

                if(obj!=null)
                {
                    if (obj.UserPwd.Equals(user.UserPwd)==true)
                    {
                        string name = "";
                        //name=dt.Rows[0]["UserName"].ToString();
                        name = obj.UserName.ToString();
                        //HttpContext.Session.SetString("username", name);
                        //Session["UserName"] = user.UserName;
                        
                    CookieOptions option = new CookieOptions();
                    if(obj.TypeId!=null)
                    Response.Cookies.Append("role", obj.TypeId.ToString(), option);
                    if (obj.UserID != null)
                    Response.Cookies.Append("userid", obj.UserID.ToString(), option);
                    if (obj.UserName != null)
                        Response.Cookies.Append("name", obj.UserName.ToString(), option);
                        
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserEmail),
                        new Claim(ClaimTypes.Email, user.UserEmail),
                        new Claim("userType","Admin")
                    };
                        //Session["UserID"] = obj.UserId.ToString();
                        //Session["UserName"] = obj.UserName.ToString();
                       var identity = new ClaimsIdentity(claims, "ZeroHungerCookie");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                    
                        if (remember==true){
                            obj.RememberMe = true;
                        }
                        var authProperties = new AuthenticationProperties
                        {
                            
                            IsPersistent = obj.RememberMe
                            
                        };
                    await _db.SaveChangesAsync();
                        string userid = "";
                        //userid = dt.Rows[0]["UserID"].ToString();
                        userid = obj.UserID.ToString();
                        //HttpContext.Session.SetString("Name", userid);
                        await HttpContext.SignInAsync("ZeroHungerCookie", claimsPrincipal, authProperties);
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        ViewData["Message"] = string.Format("Your password is incorrect.\\nPlease try again.");
                        return Page();
                    }
                }
                else
                {
                    ViewData["Message"] = string.Format("This email has not been registered yet.\\nPlease try again.");
                    return Page();
                }
                //}

            //}
        }
    }
} 
