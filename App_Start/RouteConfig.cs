﻿using System.Web.Mvc;
using System.Web.Routing;

namespace LoanCompareSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");




            routes.MapRoute(
                            name: "Default",
                            url: "{controller}/{action}/{id}",
                            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                           );



            routes.MapRoute(
                            name: "Detail",
                            url: "{controller}/{action}/{id}",
                            defaults: new { controller = "Detail", action = "Detail", id = UrlParameter.Optional }
                           );
        }
    }
}
