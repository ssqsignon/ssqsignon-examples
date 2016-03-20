
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
            SSQSignonAuthentication.Enable(this, ConfigurationManager.AppSettings["SSQSignonModuleName"], ConfigurationManager.AppSettings["SSQSignonClientId"]);

            this.RequiresAuthentication();

            Get["/hamster"] = _ =>
            {
                this.RequiresClaims(new[] { "hamster" });

                return Response.AsJson(new { message = "Hello I am the hamster" });
            };
        }
    }
}