using Logging.Service.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Logging.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "GetLogs",
                routeTemplate: "api/logs",
                defaults: new { controller = "Logs", action = "GetAll" }
            );

            config.Routes.MapHttpRoute(
                name: "GetLogsFrom",
                routeTemplate: "api/logs/from/{from}",
                defaults: new { controller = "Logs", action = "GetFrom" }
            );

            config.Routes.MapHttpRoute(
                name: "AddLog",
                routeTemplate: "api/logs/add/{level}/{message}",
                defaults: new { controller = "Logs", action = "Add" }
            );
        }
    }
}
