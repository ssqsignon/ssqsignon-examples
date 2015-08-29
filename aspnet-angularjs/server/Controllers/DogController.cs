using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace server.Controllers
{
    [Authorize(Roles="dog")]
    public class DogController : ApiController
    {
        public dynamic Get()
        {
            return Json(new { message = "Hello, I am the dog!" });
        }
    }
}
