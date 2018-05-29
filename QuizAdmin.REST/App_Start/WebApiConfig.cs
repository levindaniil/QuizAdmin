using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace QuizAdmin.REST
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "PostReport",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
