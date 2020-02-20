using LoanCompareSite.Models;
using LoanCompareSite.Models.EF;
using LoanCompareSite.Models.viewModels;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace LoanCompareSite.Controllers
{
    [HandleError]
    public class DetailController : Controller
    {

        SqlConnection _conn = new SqlConnection();
        SqlCommand _com = new SqlCommand();
        private SqlDataReader _dr;

        // GET: Detail
        //[HttpGet]
        //public ActionResult Index() {

        //    return View();
        //    }
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

                throw;
            }
            finally
            {
                _conn.Close();
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
            //            dynamic myModal = new ExpandoObject();

            try
            {
                ConnectionString();
                _conn.Open();
                _com.Connection = _conn;

                string query = $"SELECT * FROM loandetail WHERE id = {id}";
                _com = new SqlCommand(query, _conn);
                _dr = _com.ExecuteReader();

                if (_dr.HasRows == false)
                {
                    return View("Error");
                }
                while (_dr.Read())
                {

                    loanterms.Add(new Loanterms()
                    {
                        id = _dr.GetInt32(0),
                        name = _dr.GetString(1),
                        package = _dr.GetString(2),
                        count = _dr.GetInt32(5),
                        rate = _dr.GetFloat(7),
                        terms = _dr.GetString(8),
                        website = _dr.GetString(9),
                        duration = _dr.GetInt32(10)
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _conn.Close();

            }

            ViewBag.Loanterms = loanterms;
            ViewBag.Repayment = repaymentDetail;

            //            myModal.loanterms = loanterms;
            //            myModal.repayment = repaymentDetail;

            return View();
        }

        public ActionResult Selected()
        {

            try
            {
                ConnectionString();
                _conn.Open();
                _com.Connection = _conn;


                int currentCount = 0;

                currentCount = (int)Session["count"];

                string updateQuery = $"UPDATE loandetail SET count = {currentCount + 1}, date = {DateTime.Today} WHERE id = {(int)Session["selectedItemId"]}";

                _com = new SqlCommand(updateQuery, _conn);
                _com.ExecuteNonQuery();



            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            finally
            {
                _conn.Close();
            }


            return Redirect((string)Session["website"]);
        }


        private void ConnectionString()
        {
            _conn.ConnectionString = "Data Source=JOELL;Initial Catalog=loanComparer;Integrated Security=True";

        }
    }
}