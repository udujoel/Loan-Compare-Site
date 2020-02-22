using System;
using System.Collections.Generic;
using LoanCompareSite.Models.viewModels;

using Paystack.Net.SDK.Transactions;

using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LoanCompareSite.Models.EF;
using Microsoft.AspNet.Identity;
using System.Globalization;


namespace LoanCompareSite.Controllers
{
    public class SubscriptionController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {
            //check if user is subscribed already
            bool isSubscribed = false;
            bool activeSubscription = false;

            using (var db = new LoanComparerModel())
            {
                var status = db.subscriptions.Where(d => d.userid == User.Identity.GetUserName().ToLower()).ToList();
                if (status.Count()>0)
                {
                    isSubscribed = true;

                    TimeSpan diff = (TimeSpan) (status[0].enddate - status[0].startdate);



                    if (diff.Days >15)
                    {
                        activeSubscription = true;
                    }
                }
            }

            if (activeSubscription && isSubscribed)
            {
                return Redirect(Session["website"] as string);
            }

            return View();
        }



        public async Task<JsonResult> InitializePayment(PaystackCustomerModel model)
        {
            string secretKey = ConfigurationManager.AppSettings["PaystackSecret"];
            var paystackTransactionAPI = new PaystackTransaction(secretKey);
            var response = await paystackTransactionAPI.InitializeTransaction(model.email, model.amount, model.firstName, model.lastName, "https://localhost:44348/callback");
            //Note that callback url is optional
            if (response.status == true)
            {
                return Json(new { error = false, result = response }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = true, result = response }, JsonRequestBehavior.AllowGet);

        }






    }
}