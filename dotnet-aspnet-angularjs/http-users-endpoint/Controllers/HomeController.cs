using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace http_users_endpoint.Controllers
{
    public class HomeController : ApiController
    {
        public dynamic Get()
        {
            return Json("SSQ signon example http users endpoint.");
        }
    }
}
