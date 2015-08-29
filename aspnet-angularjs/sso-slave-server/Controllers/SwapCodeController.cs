using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace sso_slave_server.Controllers
{
    public class SwapCodeController : ApiController
    {
        public class PostModel
        {
            public string Code { get; set; }

            public string State { get; set; }
        }

        public dynamic Post(PostModel postModel)
        {
            var request = new RestRequest(string.Format("{0}/auth", ModuleName), Method.POST)
                .AddJsonBody(new {
                    grant_type = "authorization_code",
                    client_id = ClientId,
                    code = postModel.Code,
                    redirect_uri = SSORedirectUri
                });

            var response = RestClient.Execute<AuthorizationResponse>(request);
            if (response.ErrorException == null && response.StatusCode.Equals(HttpStatusCode.OK))
            {
                return Json(response.Data);
            }
            else
            {
                return StatusCode(HttpStatusCode.BadGateway);
            }
        }

        private class AuthorizationResponse
        {
            public string user_id { get; set; }

            public string scope { get; set; }

            public string access_token { get; set; }

            public string refresh_token { get; set; }
        }

        private string ModuleName
        {
            get { return ConfigurationManager.AppSettings["SSQSignonModuleName"]; }
        }

        private string ClientId
        {
            get { return ConfigurationManager.AppSettings["SSQSignonClientId"]; }
        }

        private string ClientSecret
        {
            get { return ConfigurationManager.AppSettings["SSQSignonClientSecret"]; }
        }

        private string SSORedirectUri
        {
            get { return "http://localhost:62326/client"; }
        }

        private RestClient RestClient
        {
            get
            {
                var restClient = new RestClient("https://tinyusers.azurewebsites.net");
                restClient.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(ClientId, ClientSecret);
                return restClient;
            }
        }
    }
}
