using Payments.DB_Context;
using Payments.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace Payments.Controllers
{
    public class AccessController : Controller
    {
        private PaymentsContext db = new PaymentsContext();
        account user = new account();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
                       
            SqlParameter[] parameters = {
               new SqlParameter("@Email", email),
               new SqlParameter("@Password", password)
            };
            try
            {
                user = db.Database.SqlQuery<account>("Access @Email, @Password", parameters).ElementAt(0);
                System.Web.HttpContext.Current.Session["LOGIN"] = user;
                return RedirectToAction("ViewPayments", "Payment");
            }
            catch(Exception ex) {     
                TempData["Error"] = "The username or password is incorrect";
                return RedirectToAction("Login","Access");
            }
        }

        public ActionResult Logout() {
            user = null;
           return RedirectToAction("Login", "Access");
        }
    }
}