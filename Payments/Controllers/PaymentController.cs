using Microsoft.Win32;
using Payments.DB_Context;
using Payments.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Payments.Controllers
{
    public class PaymentController : Controller
    {
        private PaymentsContext db = new PaymentsContext();
        private account account = System.Web.HttpContext.Current.Session["LOGIN"] as account;
        private List<int> selectedPayments = new List<int>();
        public ActionResult ViewPayments(int page = 1, int entry = 10)
        { 
            SqlParameter[] parameters = {
               new SqlParameter("@AccountReference", account.Account)
            };

            var payments = db.Database.SqlQuery<payment>("AllPaymentsByUser @AccountReference", parameters).ToList();
            payments.OrderBy(x => x.Date).ToList();

            var AllPayments = payments.Count();
            var PagePay = payments.Skip((page - 1) * entry).Take(entry).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.PageCount = (int)Math.Ceiling((double)AllPayments / entry);
            ViewBag.PageEntries = entry;
            return View(PagePay);
        }

        public ActionResult AddPayment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPayment(double amount, string description, string beneficiary)
        {
            int referenceNo;
            var pays = db.Database.SqlQuery<payment>("AllPayments").ToList();
            if (pays.Any())
            {
                referenceNo = pays.Last().ReferenceNo + 1;
            } else
            { referenceNo = 1; }
            SqlParameter[] parameters = {
                new SqlParameter("@ReferenceNo", referenceNo),
                new SqlParameter("@AccountReference", account.Account),
                new SqlParameter("@Amount", amount),
                new SqlParameter("@Description", description.ToUpper()),
                new SqlParameter("@Beneficiary", beneficiary.ToUpper()),
                new SqlParameter("@Date", DateTime.Now)
            };
            db.Database.ExecuteSqlCommand("AddPayment @ReferenceNo, @AccountReference, @Amount, @Description, @Beneficiary, @Date", parameters);
            TempData["Success"] = "the payment was added successfully";
            return RedirectToAction("AddPayment", "Payment");
        }

        public ActionResult ModifyPayment(int? referenceNo)
        {            
            payment payment = db.Database.SqlQuery<payment>("FindPayment @ReferenceNo",new SqlParameter("@ReferenceNo", referenceNo)).ToList().ElementAt(0);
            return View(payment);
        }

        [HttpPost]
        public ActionResult ModifyPayment(payment payment)
        {                          
            SqlParameter[] parameters = {
                new SqlParameter("@ReferenceNo", payment.ReferenceNo),
                new SqlParameter("@Amount", payment.Amount),
                new SqlParameter("@Description", payment.Description.ToUpper()),
                new SqlParameter("@Beneficiary", payment.Beneficiary.ToUpper()),
            };
            db.Database.ExecuteSqlCommand("ModifyPayment @ReferenceNo, @Amount, @Description, @Beneficiary", parameters);
            TempData["Updated"] = "The payment has been updated successfully";
            return RedirectToAction("ViewPayments", "Payment");
        }

        [HttpPost]
        public ActionResult DeletePayment(int? referenceNo)
        {
            db.Database.ExecuteSqlCommand("DeletePayment @ReferenceNo", new SqlParameter("@ReferenceNo", referenceNo));
            TempData["Deleted"] = "The payment has been deleted successfully";
            return RedirectToAction("ViewPayments", "Payment");
        }        

        [HttpPost]
        public ActionResult DeleteSelectedPayments(int[] referenceNo)
        {
            foreach (int n in referenceNo)
            {
                db.Database.ExecuteSqlCommand("DeletePayment @ReferenceNo", new SqlParameter("@ReferenceNo", n));
            }            
            TempData["Deleted"] = "The payments have been deleted successfully";
            return RedirectToAction("ViewPayments", "Payment");
        }

       
    }
}