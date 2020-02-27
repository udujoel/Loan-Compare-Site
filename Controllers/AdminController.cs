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
            string mostSubscribedPackage;
            string mostVisitedPackage;
            int totalVisits;
            double averageAmountRequested;
            int mostDurationRequested;
            int mostAmountRequested;


            try
            {
                using (var db = new LoanComparerModel())
                {
                    allProviders = db.loandetails.ToList();
                    visitcount = db.visitcounts.ToList();


                    foreach (var provider in allProviders.OrderBy(x => x.id))
                    {
                        int visitSum = db.visitcounts.Where(d => d.packageid == provider.id)
                                         .Select(d => d.visits)
                                         .DefaultIfEmpty(0)
                                         .Sum();
                        var uniqueCount = db.visitcounts.Where(x => x.packageid == provider.id).Distinct().Count();
                        adminreport.Add(new adminReport()
                        {
                            noOfVisits = (int)visitSum,
                            package = provider.name,
                            providername = provider.name,
                            uniqueVisit = uniqueCount
                        });


                    }
                    //mostSubscribed package
                    var mostVisits = db.loandetails.Max(x => x.count).GetValueOrDefault(0);
                    mostSubscribedPackage = db.loandetails.Where(x => x.count == mostVisits).Select(x => x.package).FirstOrDefault();

                    //mostVisited Package
                    var mostVisitedPackage_query = db.visitcounts.Max(x => x.visits);
                    mostVisitedPackage = db
                                         .loandetails
                                         .Find(db.visitcounts.Where(x => x.visits == mostVisitedPackage_query)
                                         .Select(x => x.packageid))
                                         .package;

                    //totalVisits
                    totalVisits = db.visitcounts.Sum(x => x.visits);

                    //averageAmountRequested
                    var sumOfAmounts = db.requests.Sum(x => x.amountreq);
                    var requestCount = db.visitcounts.Count();

                    averageAmountRequested = sumOfAmounts / requestCount;

                    //mostDurationRequested
                    mostDurationRequested = (int)db.requests.Max(x => x.durationreq);

                    //mostAmountRequested
                    mostAmountRequested = (int)db.requests.Max(x => x.amountreq);

                }

                ViewBag.mostSubscribedPackage = mostSubscribedPackage;
                ViewBag.mostVisitedPackage = mostVisitedPackage;
                ViewBag.totalVisits = totalVisits;
                ViewBag.averageAmountRequested = averageAmountRequested;
                ViewBag.mostDurationRequested = mostDurationRequested;
                ViewBag.mostAmountRequested = mostAmountRequested;

            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View(adminreport);
        }
    }
}