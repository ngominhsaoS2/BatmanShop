using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BatmanShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{*botdetect}",
            new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Products in Category",
                url: "Product/{MetaTitle}-{productCategoryId}",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new[] { "BatmanShop.Controllers" }
            );

            routes.MapRoute(
                name: "Product Detail",
                url: "Product/{CategoryMetaTitle}/{ProductMetaTitle}-{id}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "BatmanShop.Controllers" }
            );

            routes.MapRoute(
                name: "Add to cart",
                url: "Add-To-Cart",
                defaults: new { controller = "Cart", action = "AddNew", id = UrlParameter.Optional },
                namespaces: new[] { "BatmanShop.Controllers" }
            );

            routes.MapRoute(
                name: "Cart",
                url: "Cart",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BatmanShop.Controllers" }
            );

            routes.MapRoute(
                name: "Pay",
                url: "Pay",
                defaults: new { controller = "Cart", action = "Pay", id = UrlParameter.Optional },
                namespaces: new[] { "BatmanShop.Controllers" }
            );

            routes.MapRoute(
                name: "Successful payment",
                url: "Successful",
                defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
                namespaces: new[] { "BatmanShop.Controllers" }
            );

            routes.MapRoute(
                name: "Register",
                url: "Register",
                defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
                namespaces: new[] { "BatmanShop.Controllers" }
            );

            routes.MapRoute(
                name: "Member Login",
                url: "Login",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "BatmanShop.Controllers" }
            );

            routes.MapRoute(
                name: "Member Logout",
                url: "Logout",
                defaults: new { controller = "User", action = "Logout", id = UrlParameter.Optional },
                namespaces: new[] { "BatmanShop.Controllers" }
            );

            routes.MapRoute(
                name: "Search",
                url: "Search",
                defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
                namespaces: new[] { "BatmanShop.Controllers" }
            );

            routes.MapRoute(
                name: "News",
                url: "News",
                defaults: new { controller = "Content", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BatmanShop.Controllers" }
            );

            routes.MapRoute(
                name: "News detail",
                url: "News/{MetaTitle}-{id}",
                defaults: new { controller = "Content", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "BatmanShop.Controllers" }
            );

            routes.MapRoute(
                name: "Tag",
                url: "Tag/{tagId}",
                defaults: new { controller = "Content", action = "Tag", id = UrlParameter.Optional },
                namespaces: new[] { "BatmanShop.Controllers" }
            );

            routes.MapRoute(
                name: "Contact",
                url: "Contact",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BatmanShop.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BatmanShop.Controllers" }
            );
        }
    }
}
