using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security;

namespace http_users_endpoint
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseBasicAuthentication(new Thinktecture.IdentityModel.Owin.BasicAuthenticationOptions(string.Empty, 
                new Thinktecture.IdentityModel.Owin.BasicAuthenticationMiddleware.CredentialValidationFunction(BasicAuthenticate)));
        }

        private System.Threading.Tasks.Task<IEnumerable<System.Security.Claims.Claim>> BasicAuthenticate(string username, string password)
        {
            return System.Threading.Tasks.Task.FromResult(username.Equals("example") && password.Equals("testtest")
                ? new System.Security.Claims.Claim[] { new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, "root") }.AsEnumerable()
                : null);
        }
    }
}
