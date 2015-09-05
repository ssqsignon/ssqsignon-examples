using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace ExampleUsersEndpointHttp.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        public class PostModel
        {
            public string Username { get; set; }

            public string Password { get; set; }

            public string Scope { get; set; }
        }

        public dynamic Post(PostModel model)
        {
            var user = Users.Name(model.Username);
            if (user == null || (!string.IsNullOrEmpty(model.Password) && !user.PasswordValid(model.Password)))
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.Content = new StringContent("invalid_grant");
                return response;
            }
            return Json(new { id = user.Username, scope = user.IntersectScope(model.Scope) });
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
}
