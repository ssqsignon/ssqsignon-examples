using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace server
{
	public partial class Startup
	{
		public void ServeClientFiles(IAppBuilder app)
        {
            app.UseFileServer();
        }
	}
}