using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.ModelBinding;
using RestSharp;
using System.Configuration;

namespace master_app
{
    public class AuthModule : SSQSignon.AuthProxyModule
    {
        public AuthModule()
            :base("auth", ConfigurationManager.AppSettings["SSQSignonModuleName"], ConfigurationManager.AppSettings["SSQSignonClientId"], null)
        {
        }
    }
}