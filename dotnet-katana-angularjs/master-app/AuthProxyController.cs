using System.Configuration;

namespace master_app
{
    public class AuthProxyController : SSQsignon.ProxyController
    {
        public AuthProxyController()
            :base(ConfigurationManager.AppSettings["SSQSignonModuleName"], ConfigurationManager.AppSettings["SSQSignonClientId"], null)
        {
        }
    }
}