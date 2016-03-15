using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sso_slave_server
{
    public partial class Startup
    {
        public void ServeClientFiles(IAppBuilder app)
        {
            app.UseFileServer();
        }
    }
}