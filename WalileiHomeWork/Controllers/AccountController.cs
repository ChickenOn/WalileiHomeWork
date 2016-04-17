using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WalileiHomeWork.Models;
using System.Web.Security;

namespace WalileiHomeWork.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel data)
        {
            // 登入時清空所有 Session 資料
            Session.RemoveAll();

            string roles;
            int customerId;
            if (ValidateLogin(data.Email, data.Password, out roles, out customerId))
            {
                FormsAuthentication.RedirectFromLoginPage(data.Email, false);

                // 將管理者登入的 Cookie 設定成 Session Cookie
                bool isPersistent = false;

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                  data.Email,
                  DateTime.Now,
                  DateTime.Now.AddMinutes(30),
                  isPersistent,
                  roles,
                  FormsAuthentication.FormsCookiePath);

                string encTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie.
                Response.SetCookie(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));


                if (roles.Contains("sysadmin"))
                {
                    return RedirectToAction("Index", "Home");
                }

                var repo = RepositoryHelper.Get客戶資料Repository();

                return RedirectToAction("Edit", "Customer", new { id = customerId });
            }

            return View();
        }
        
       

        private bool ValidateLogin(string mail, string pwd, out string roles, out int customerId)
        {
            customerId = -1;
            roles = string.Empty;

            // 驗證
            if (mail == "admin" && pwd == "123")
            {
                roles = "sysadmin";
                return true;
            }

            // 請自行寫 Code 檢查 Username, Password 是否正確
            var repo = RepositoryHelper.Get客戶資料Repository();
            var customer = repo.Where(c => c.Email == mail).SingleOrDefault();
            if (FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "SHA1") == "")
            {
                customerId = customer.Id;
                roles = "customer";
                return true;
            }

            return false;
        }
        
    }
}