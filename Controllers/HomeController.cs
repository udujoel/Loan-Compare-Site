using System.Web.Mvc;

namespace LoanCompareSite.Controllers
{

    [HandleError]
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }


    }
}