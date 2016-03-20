using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Configuration;

namespace sso_slave_server
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseSSQSignonAuthentication(ConfigurationManager.AppSettings["SSQSignonServerName"]);
        }
    }
}
