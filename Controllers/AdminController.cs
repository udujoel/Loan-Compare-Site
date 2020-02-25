using LoanCompareSite.Models.EF;
using LoanCompareSite.Models.viewModels;

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

            var adminreport = new List<adminReport>();


            try
            {
                using (var db = new LoanComparerModel())
                {
                    allProviders = db.loandetails.ToList();
                    visitcount = db.visitcounts.ToList();


                    foreach (var provider in allProviders.OrderBy(x => x.id))
                    {
                        int visitSum = db.visitcounts.Where(d => d.packageid == provider.id).Select(d => d.visits).Sum();
                        var uniqueCount = db.visitcounts.Where(x => x.packageid == provider.id).Distinct().Count();
                        adminreport.Add(new adminReport() { noOfVisits = visitSum, package = provider.name, providername = provider.name, uniqueVisit = uniqueCount });


                    }
                }
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View(adminreport);
        }
    }
}