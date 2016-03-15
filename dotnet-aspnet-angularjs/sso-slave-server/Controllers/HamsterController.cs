using Microsoft.Owin.Security.SSQSignon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace sso_slave_server.Controllers
{
    [SSQSignonAuthentication, Authorize(Roles="hamster")]
    public class HamsterController : ApiController
    {
        public dynamic Get()
        {
            return Json(new { message = "Hi, I am the hamster!" });
        }
    }
}
