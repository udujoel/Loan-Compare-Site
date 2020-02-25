using System;
using System.Web.Mvc;
using LoanCompareSite.Models.EF;

namespace LoanCompareSite.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            
            try
            {

                using (var db = new  LoanComparerModel())
                {
                    


                }


            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View();
        }
    }
}