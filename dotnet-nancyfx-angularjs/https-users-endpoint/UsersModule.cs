
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Security;
using Nancy.ModelBinding;

namespace https_users_endpoint
{
    public class UsersModule : Nancy.NancyModule
    {
        public class PostModel
        {
            public string Username { get; set; }

            public string Password { get; set; }

            public string Scope { get; set; }
        }

        public UsersModule()
        {
            Nancy.Authentication.Basic.BasicAuthentication.Enable(this, new Nancy.Authentication.Basic.BasicAuthenticationConfiguration(new BasicValidator(), "users"));
            this.RequiresAuthentication();

            Post["/users"] = _ =>
            {
                var model = this.Bind<PostModel>();
                var user = Users.Name(model.Username);

                if (user == null || (!string.IsNullOrEmpty(model.Password) && !user.PasswordValid(model.Password)))
                {
                    var response = Response.AsText("invalid_grant");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                return Response.AsJson(new { id = user.Username, scope = user.IntersectScope(model.Scope) });
            };
        }

        private IEnumerable<User> Users
        {
            get
            {
                return new User[]
                {
                    new User { Username = "test1@users.com", Password = "testtest1", Permissions = new string[] { "cat" } },
                    new User { Username = "test2@users.com", Password = "testtest2", Permissions = new string[] { "cat", "dog" } },
                };
            }
        }
    }

    internal class User
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public IEnumerable<string> Permissions { get; set; }

        public bool PasswordValid(string candidate)
        {
            return Password.Equals(candidate);
        }

        public string IntersectScope(string requestedScope)
        {
            return string.Join(" ", string.IsNullOrEmpty(requestedScope) ? Permissions : requestedScope.Split(' ').Intersect(Permissions));
        }
    }

    internal static class UserExtensions
    {
        public static User Name(this IEnumerable<User> users, string name)
        {
            return string.IsNullOrEmpty(name) ? null : users.SingleOrDefault(u => u.Username.Equals(name));
        }
    }

    internal class BasicValidator : Nancy.Authentication.Basic.IUserValidator
    {
        public IUserIdentity Validate(string username, string password)
        {
            return "example".Equals(username) && "testtest".Equals(password) ? new UserIdentityAdapter() : null;
        }

        private class UserIdentityAdapter : Nancy.Security.IUserIdentity
        {
            public IEnumerable<string> Claims { get { return new string[] { }; } }

            public string UserName { get { return "example"; } }
        }
    }
}