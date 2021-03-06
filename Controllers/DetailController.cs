﻿using LoanCompareSite.Models;
using LoanCompareSite.Models.EF;
using LoanCompareSite.Models.viewModels;

using Microsoft.AspNet.Identity;

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

                using (var db = new LoanComparerModel())
                {
                    //save the request
                    string email;
                    if (Request.IsAuthenticated)
                    {
                        email = User.Identity.GetUserName();
                    }
                    else
                    {
                        email = "annonymouse";

                    }

                    db.requests.Add(new request()
                    {
                        amountreq = loanRequest.amount,
                        durationreq = loanRequest.duration,
                        username = email
                    });


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

                    db.SaveChanges();

                }

            }
            catch (Exception e)
            {
                return View("Error");


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
                using (var db = new LoanComparerModel())
                {

                    //increment count


                    var visitsQuery = db.visitcounts.Where(d => d.packageid == id).FirstOrDefault();


                    if (visitsQuery == null)
                    {
                    db.visitcounts.Add(new visitcount() 
                                           {username = User.Identity.GetUserName(), visits = 1, packageid = id});

                }
                    else
                    {
                       

                        db.visitcounts.Find(visitsQuery.id).visits += 1;
                }



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

                    db.SaveChanges();

                }

        }
            catch (Exception e)
            {
                return View("Error");

    }


    ViewBag.Loanterms = loanterms;
            ViewBag.Repayment = repaymentDetail;



            return View();
        }

        public ActionResult Selected()
        {



            return RedirectToRoute(new { Controller = "Subscription", action = "Index" });
        }



    }
}