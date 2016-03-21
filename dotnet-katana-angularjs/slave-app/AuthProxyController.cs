using System.Configuration;

namespace slave_app
{
    public class AuthProxyController : SSQsignon.ProxyController
    {
        public AuthProxyController()
            :base(ConfigurationManager.AppSettings["SSQSignonServerName"], ConfigurationManager.AppSettings["SSQSignonAppId"], ConfigurationManager.AppSettings["SSQSignonAppSecret"])
        {
        }
    }
}