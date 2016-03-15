using Microsoft.Owin.Security.SSQSignon;
using System.Web.Http;

namespace slave_app
{
    [SSQSignonAuthentication, Authorize(Roles="hamster")]
    public class HamsterController : ApiController
    {
        public dynamic Get()
        {
            return Json(new { message = string.Format("Hello {0}, I am the hamster!", User.Identity.Name) });
        }
    }
}
