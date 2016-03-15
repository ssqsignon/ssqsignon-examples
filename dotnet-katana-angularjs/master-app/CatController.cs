using Microsoft.Owin.Security.SSQSignon;
using System.Web.Http;

namespace master_app
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
