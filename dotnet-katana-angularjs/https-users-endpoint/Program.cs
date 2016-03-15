using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace https_users_endpoint
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:9903";

            WebApp.Start(url, appBuilder =>
            {
                appBuilder.UseBasicAuthentication(new Thinktecture.IdentityModel.Owin.BasicAuthenticationOptions(string.Empty,
                    new Thinktecture.IdentityModel.Owin.BasicAuthenticationMiddleware.CredentialValidationFunction(BasicAuthenticate)));

                appBuilder.UseWebApi(Routes());
            });

            Console.WriteLine(string.Format("Listening at {0}", url));
            Console.ReadLine();
        }

        private static HttpConfiguration Routes()
        {
            HttpConfiguration config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            return config;
        }

        private static System.Threading.Tasks.Task<IEnumerable<System.Security.Claims.Claim>> BasicAuthenticate(string username, string password)
        {
            return System.Threading.Tasks.Task.FromResult(username.Equals("example") && password.Equals("testtest")
                ? new System.Security.Claims.Claim[] { new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, "root") }.AsEnumerable()
                : null);
        }
    }
}
