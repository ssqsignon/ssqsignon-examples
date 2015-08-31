using SSQSignon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace sso_slave_server.Controllers
{
    public class AuthController : AuthProxyController
    {
        public AuthController()
            :base(ConfigurationManager.AppSettings["SSQSignonModuleName"], ConfigurationManager.AppSettings["SSQSignonClientId"], ConfigurationManager.AppSettings["SSQSignonClientSecret"])
        {
        }
    }
}
