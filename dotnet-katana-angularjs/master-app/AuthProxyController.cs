using System.Configuration;

namespace master_app
{
    public class AuthProxyController : SSQsignon.ProxyController
    {
        public AuthProxyController()
            :base(ConfigurationManager.AppSettings["SSQSignonServerName"], ConfigurationManager.AppSettings["SSQSignonAppId"], null)
        {
        }
    }
}