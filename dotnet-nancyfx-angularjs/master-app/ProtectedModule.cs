
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Security;
using Nancy.ModelBinding;
using Nancy.Authentication.SSQSignon;
using System.Configuration;

namespace master_app
{
    public class ProtectedModule : Nancy.NancyModule
    {
        public ProtectedModule()
        {
            SSQSignonAuthentication.Enable(this, ConfigurationManager.AppSettings["SSQSignonServerName"], ConfigurationManager.AppSettings["SSQSignonAppId"]);

            this.RequiresAuthentication();

            Get["/cat"] = _ =>
            {
                this.RequiresClaims(new[] { "cat" });

                return Response.AsJson(new { message = "Hello I am the cat" });
            };

            Get["/dog"] = _ =>
            {
                this.RequiresClaims(new[] { "dog" });

                return Response.AsJson(new { message = "Hello I am the dog" });
            };
        }
    }
}