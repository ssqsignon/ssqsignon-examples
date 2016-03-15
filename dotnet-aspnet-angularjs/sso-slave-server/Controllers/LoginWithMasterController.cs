using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace sso_slave_server.Controllers
{
    public class LoginWithMasterController : ApiController
    {
        public dynamic Get(string state)
        {
            var redirectUri = new UriBuilder("http://localhost:9901");

            redirectUri.Query = string.Join("&", new Dictionary<string, string>
            {
                { "redirect_uri", "http://localhost:9902" },
                { "scope", "hamaster" },
                { "client_id", ConfigurationManager.AppSettings["SSQSignonClientId"] },
                { "state", state }
            }.Select(kv => string.Format("{0}={1}", kv.Key, kv.Value)));


            return Redirect(redirectUri.ToString());
        }
    }
}
