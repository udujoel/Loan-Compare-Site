using LoanCompareSite.Models.EF;

using Microsoft.AspNet.Identity;

using Paystack.Net.SDK.Transactions;

using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoanCompareSite.Controllers
{
    public class CallbackController : Controller
    {
        //        [Route("order/callback")]
        public async Task<ActionResult> Index()
        {
            string secretKey = ConfigurationManager.AppSettings["PaystackSecret"];
            var paystackTransactionAPI = new PaystackTransaction(secretKey);
            var tranxRef = HttpContext.Request.QueryString["reference"];
            if (tranxRef != null)
            {
                var response = await paystackTransactionAPI.VerifyTransaction(tranxRef);
                if (response.status)
                {

                    try
                    {
                        using (var db = new LoanComparerModel())
                        {

                            int currentCount = 0;
                            currentCount = (int)Session["count"];

                            db.loandetails.Find((int)Session["selectedItemId"]).count = currentCount + 1;
                            db.loandetails.Find((int)Session["selectedItemId"]).date = DateTime.Now;
                            var user = new subscription();
                            user.userid = User.Identity.GetUserName().ToLower();
                            user.startdate = DateTime.Now;
                            user.enddate = DateTime.Now.AddMonths(1);
                            db.subscriptions.Add(user);

                            db.SaveChanges();

                        }
                    }
                    catch (Exception e)
                    {

                        return View("Error");
                    }

                    return View(response);
                }
            }

            return View("PaymentError");
        }

        public ActionResult Callback()
        {


            return View("Index");
        }
    }
}