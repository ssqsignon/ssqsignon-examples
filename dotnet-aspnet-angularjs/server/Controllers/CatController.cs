using Microsoft.Owin.Security.SSQSignon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace server.Controllers
{
    [SSQSignonAuthentication, Authorize(Roles = "cat")]
    public class CatController : ApiController
    {
        public dynamic Get()
        {
            return Json(new { message = string.Format("Hello {0}, I am the cat!", User.Identity.Name) });
        }
    }
}
