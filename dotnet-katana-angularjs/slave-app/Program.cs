using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using System.Configuration;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.FileSystems;
using System.Web.Http;

namespace slave_app
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:9902";

            WebApp.Start(url, appBuilder =>
            {
                appBuilder.UseSSQSignonAuthentication(ConfigurationManager.AppSettings["SSQSignonServerName"]);
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
