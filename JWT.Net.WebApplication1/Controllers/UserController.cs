using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT.Net.WebApplication1.Attr;
using JWT.Net.WebApplication1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Net.WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [JWTAttribute]
        public string Auth(string userName, string password)
        {
            return "OK";
        }

        [JWTAttribute(true)]
        public UserModel Get()
        {
            var jwtToken = RouteData.Values["jwtToken"].ToString();
            var password = RouteData.Values["password"].ToString();

            var jwt = JWTPackage<UserModel>.Parse(jwtToken, password);

            return jwt.Payload.Data;
        }
    }
}
