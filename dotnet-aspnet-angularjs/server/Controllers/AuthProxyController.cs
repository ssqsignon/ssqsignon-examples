using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace server.Controllers
{
    public class AuthProxyController : SSQsignon.ProxyController
    {
        public AuthProxyController()
            :base(ConfigurationManager.AppSettings["SSQSignonServerName"], ConfigurationManager.AppSettings["SSQSignonAppId"], null)
        {
        }
    }
}