
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace master_app
{
    public class ClientModule : Nancy.NancyModule
    {
        public ClientModule()
        {
            Get["/"] = _ => Response.AsFile("Client/index.html");
            Get["/app.js"] = _ => Response.AsFile("Client/app.js");
        }
    }
}