using LoanCompareSite.Models;
using LoanCompareSite.Models.EF;
using LoanCompareSite.Models.viewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace LoanCompareSite.Controllers
{
    [HandleError]
    public class DetailController : Controller
    {


        [HttpPost]
        public ActionResult Index(string amount)
        {

            return RedirectToAction("Detail",
                new RouteValueDictionary(new { controller = "Detail", action = "Detail", Amount = amount }));
        }

        [ValidateAntiForgeryToken]
        public ActionResult Detail(LoanRequest loanRequest)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            Session["amount"] = loanRequest.amount;
            Session["duration"] = loanRequest.duration;
            List<LoansDetail> loansDetail = new List<LoansDetail>();
            long minAmount, maxAmount;
            int maxDuration;

            try
            {

                using (var db = new LoanComparerDBModel())
                {



                    var query = from d in db.loandetails
                                orderby d.id
                                select d;

                    foreach (var detail in query)
                    {
                        minAmount = detail.minAmount;
                        maxAmount = detail.maxAmount;
                        maxDuration = detail.duration;

                        if (loanRequest.amount >= minAmount && loanRequest.amount <= maxAmount && loanRequest.duration <= maxDuration)
                        {
                            loansDetail.Add(new LoansDetail()
                            {


                                id = detail.id,
                                name = detail.name,
                                package = detail.package

                            });
                        }

                    }

                    if (loansDetail.Count < 1)
                    {
                        ViewBag.Error = "Sorry. None of Our Providers Can Provide Your Request At the Moment. Pls Try another request.";
                        return View(loansDetail);
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);


            }


            return View(loansDetail);
        }


        [Authorize]
        public ActionResult Loanterms(int id)
        {

            int userDuration = (int)Session["duration"];
            long userAmount = (long)Session["amount"];

            List<Loanterms> loanterms = new List<Loanterms>();

            List<RepaymentDetail> repaymentDetail = new List<RepaymentDetail>();

            try
            {
                using (var db = new LoanComparerDBModel())
                {


                    var query = db.loandetails.Where(d => d.id == id).ToList();


                    if (query.Count == 0)
                    {
                        return View("Error");
                    }

                    foreach (var detail in query)
                    {

                        loanterms.Add(new Loanterms()
                        {
                            id = detail.id,
                            name = detail.name,
                            package = detail.package,
                            count = (int)detail.count,
                            rate = detail.rate,
                            terms = detail.terms,
                            website = detail.website,
                            duration = detail.duration
                        });

                    }




                    ViewBag.provider = loanterms[0].name;
                    ViewBag.package = loanterms[0].package;

                    int count = 1;

                    while (count < userDuration)
                    {
                        if (userDuration > 3)
                        {
                            loanterms[0].rate += 2;
                        }

                        count += 3;
                    }



                    //for repayment table:

                    double repaymentOnPrincipal = (userAmount) / userDuration;
                    double repaymentOnInterestPerYear = (userAmount * loanterms[0].rate * 0.01) / 12;
                    double monthlyLoanDue = repaymentOnPrincipal + repaymentOnInterestPerYear;


                    double amountPaid = monthlyLoanDue;
                    Session["monthly-due"] = monthlyLoanDue;
                    double total = monthlyLoanDue * userDuration;


                    int rCount = 0;
                    while (rCount < userDuration)
                    {
                        repaymentDetail.Add(new RepaymentDetail()
                        {
                            monthno = rCount + 1,
                            amountToPay = Math.Round(monthlyLoanDue),
                            payPercent = (int)Math.Round((amountPaid / total) * 100),
                            total = total,
                            balance = total - amountPaid,
                        });

                        amountPaid += monthlyLoanDue;
                        rCount += 1;
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            ViewBag.Loanterms = loanterms;
            ViewBag.Repayment = repaymentDetail;



            return View();
        }

        public ActionResult Selected()
        {

            try
            {
                using (var db = new LoanComparerDBModel())
                {

                    int currentCount = 0;
                    currentCount = (int)Session["count"];

                    db.loandetails.Find((int)Session["selectedItemId"]).count = currentCount + 1;
                    db.loandetails.Find((int)Session["selectedItemId"]).date = DateTime.Now;

                    db.SaveChanges();

                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            return RedirectToRoute(new { Controller = "Subscription", action = "Index" });
            //            return Redirect((string)Session["website"]);
        }



    }
}