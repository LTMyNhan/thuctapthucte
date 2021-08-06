using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace tt
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /* routes.MapRoute(
                 "TTVTbyMonthYear",
                 "ttvt/bymonthyear/{month}/{year}",
                 new { Controller = "TTVT", action = "byMonthYear", month = "01", year = "2020"},
                 new { month = @"\d{2}", year = @"\d{4}"});
            */
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "DashBoard", action = "DashBoardByDataType", id = UrlParameter.Optional }
            );
        }
    }
}
