using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoanCompareSite.Controllers
{
    public class ManageUserRolesController : Controller
    {
        // GET: ManageUserRoles
        public ActionResult ManageRoles()
        {
            return View();
        }
        
        public ActionResult CreateRole()
        {
            return View();
        }
    }
}