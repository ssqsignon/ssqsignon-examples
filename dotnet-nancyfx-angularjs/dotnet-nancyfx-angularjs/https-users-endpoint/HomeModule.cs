
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace https_users_endpoint
{
    public class HomeModule : Nancy.NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ =>
            {
                return Response.AsText("SSQ signon example https users endpoint.");
            };
        }
    }
}