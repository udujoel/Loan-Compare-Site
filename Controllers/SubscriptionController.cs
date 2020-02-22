using LoanCompareSite.Models.viewModels;

using Paystack.Net.SDK.Transactions;

using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace LoanCompareSite.Controllers
{
    public class SubscriptionController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {
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