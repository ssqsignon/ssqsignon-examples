
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using System.Configuration;
using System.Collections.Specialized;

namespace slave_app
{
    public class ClientModule : Nancy.NancyModule
    {
        public ClientModule()
        {
            Get["/"] = _ => Response.AsFile("Client/index.html");
            Get["/app.js"] = _ => Response.AsFile("Client/app.js");

            Get["/loginwithmaster"] = _ =>
            {
                var redirectUri = new UriBuilder("http://localhost:9901");

                var query =

                redirectUri.Query = string.Join("&", new Dictionary<string, string>
                {
                    { "redirect_uri", "http://localhost:9902" },
                    { "scope", "hamaster" },
                    { "client_id", ConfigurationManager.AppSettings["SSQSignonAppId"] },
                    { "state", Request.Query.state }
                }.Select(kv => string.Format("{0}={1}", kv.Key, kv.Value)));

                return Response.AsRedirect(redirectUri.ToString());
            };
        }
    }
}