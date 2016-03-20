using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace sso_slave_server.Controllers
{
    public class AuthProxyController : SSQsignon.ProxyController
    {
        public AuthProxyController()
            : base(ConfigurationManager.AppSettings["SSQSignonServerName"], ConfigurationManager.AppSettings["SSQSignonAppId"], ConfigurationManager.AppSettings["SSQSignonAppSecret"])
        {
        }
    }
}
