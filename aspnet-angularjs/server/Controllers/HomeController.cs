using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace server.Controllers
{
    public class HomeController : ApiController
    {
        public dynamic Get()
        {
            return Redirect(new Uri("/client", UriKind.Relative));
        }
    }
}
