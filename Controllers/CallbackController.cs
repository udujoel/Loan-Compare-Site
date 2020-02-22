using Paystack.Net.SDK.Transactions;

using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoanCompareSite.Controllers
{
    public class CallbackController : Controller
    {
        [Route("order/callback")]
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
                    return View(response);
                }
            }

            return View("PaymentError");
        }
    }
}