using LoanCompareSite.Models;

using System.Linq;
using System.Web.Mvc;

namespace LoanCompareSite.Controllers
{

    public class RolesController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        // GET:Roles
        public ActionResult Index()
        {

            return View(context.Roles.ToList());
        }

        //        public ActionResult ManageUsers()
        //        {
        //            // prepopulat roles for the view dropdown
        //            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr =>
        //            new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
        //            ViewBag.Roles = list;
        //            return View();
        //        }
        //        
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult RoleAddToUser(string UserName, string RoleName)
        //        {
        //            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        //            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
        //            manager.AddToRole(user.Id, RoleName);
        //        
        //            //ViewBag.ResultMessage = "Role created successfully !";
        //        
        //            // prepopulat roles for the view dropdown
        //            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
        //            ViewBag.Roles = list;
        //        
        //            return View("ManageUsers");
        //        }
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult GetRoles(string UserName)
        //        {
        //            if (!string.IsNullOrWhiteSpace(UserName))
        //            {
        //                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        //                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
        //        
        //                ViewBag.RolesForThisUser = manager.GetRoles(user.Id);
        //        
        //                // prepopulat roles for the view dropdown
        //                var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
        //                ViewBag.Roles = list;
        //            }
        //        
        //            return View("ManageUsers");
        //        }
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        //        {
        //            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
        //        
        //            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        //        
        //            if (manager.IsInRole(user.Id, RoleName))
        //            {
        //                manager.RemoveFromRole(user.Id, RoleName);
        //                ViewBag.ResultMessage = "Role removed from this user successfully !";
        //            }
        //            else
        //            {
        //                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
        //            }
        //            // prepopulat roles for the view dropdown
        //            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
        //            ViewBag.Roles = list;
        //        
        //            return View("ManageUsers");
        //        }

    }
}