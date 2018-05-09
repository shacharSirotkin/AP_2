using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace A_mazingWebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
              name: "UpdateScores",
              routeTemplate: "api/Users/{action}/{userName}/{condition}",
              defaults: new { controller = "Users" }
            );

            config.Routes.MapHttpRoute(
                name: "Generate",
                routeTemplate: "api/{controller}/{name}/{rows}/{cols}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Solve",
                routeTemplate: "api/{controller}/{name}/{algorithm}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "GetUser",
                routeTemplate: "api/{controller}/{id}/{password}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
