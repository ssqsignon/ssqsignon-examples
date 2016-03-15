using Microsoft.Owin.Hosting;
using Owin;
using System;
using SSQsignon;
using System.Configuration;
using System.Web.Http;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;

namespace master_app
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:9901";

            WebApp.Start(url, appBuilder =>
            {
                appBuilder.UseSSQSignonAuthentication(ConfigurationManager.AppSettings["SSQSignonModuleName"]);
                appBuilder.UseFileServer(new FileServerOptions { RequestPath = new Microsoft.Owin.PathString(""), FileSystem = new PhysicalFileSystem("./Client") });
                appBuilder.UseWebApi(Routes());
            });

            Console.WriteLine(string.Format("Listening at {0}", url));
            Console.ReadLine();
        }

        private static HttpConfiguration Routes()
        {
            HttpConfiguration config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "Auth",
                routeTemplate: "auth/{*command}",
                defaults: new { controller = "AuthProxy" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            return config;
        }
    }
}
