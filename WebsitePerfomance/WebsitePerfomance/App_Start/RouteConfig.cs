﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebsitePerfomance
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "WebsitePerfomance", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Sitemap",
                url: "sitemap.xml",
                defaults: new { controller = "SiteController", action = "Measure" }
            );
        }
    }
}
