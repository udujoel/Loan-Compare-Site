using LoanCompareSite.Models.EF;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LoanCompareSite.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            List<loandetail> allProviders;
            List<visitcount> visitcount;


            try
            {
                using (var db = new LoanComparerModel())
                {
                    allProviders = db.loandetails.ToList();
                    visitcount = db.visitcounts.ToList();
                }
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View(allProviders);
        }
    }
}