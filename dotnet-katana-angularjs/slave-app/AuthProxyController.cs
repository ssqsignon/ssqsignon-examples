using System.Configuration;

namespace slave_app
{
    public class AuthProxyController : SSQsignon.ProxyController
    {
        public AuthProxyController()
            :base(ConfigurationManager.AppSettings["SSQSignonModuleName"], ConfigurationManager.AppSettings["SSQSignonClientId"], ConfigurationManager.AppSettings["SSQSignonClientSecret"])
        {
        }
    }
}