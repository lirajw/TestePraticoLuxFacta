using System.Web.Http;
using Ninject;
using LuxFactaTestePratico.Interfaces;
using System.Reflection;

namespace LuxFactaTestePratico
{
    public static class WebApiConfig
    {
        public static StandardKernel Kernel { get; private set; }
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.Indent = true;

            Kernel = new StandardKernel();
            WebApiConfig.Kernel.Load(Assembly.GetExecutingAssembly());

        }
    }
}
