using JWT.Net.WebApplication1.Models;

using Microsoft.AspNetCore.Mvc.Filters;

using System;
using System.Collections.Generic;

namespace JWT.Net.WebApplication1.Attr
{
    public class JWTAttribute : ActionFilterAttribute
    {
        bool _valide = false;

        string _pwd = "yswenli";

        ValueTuple<string, string> _keyValuePair;

        public JWTAttribute(bool valide = false)
        {
            _valide = valide;

            var jwtp = new JWTPackage<UserModel>(new UserModel()
            {
                Id = "1",
                Name = "yswenli",
                Roles = new List<string> { "Admin" }
            }, 180, _pwd);

            _keyValuePair = jwtp.GetAuthorizationBearer();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (_valide)
            {
                if (context.HttpContext.Request.Headers.ContainsKey(_keyValuePair.Item1))
                {
                    var val = context.HttpContext.Request.Headers[_keyValuePair.Item2].ToString();

                    val = val.Replace(JWTPackage.Prex, "");

                    var jwt = JWTPackage<UserModel>.Parse(val, _pwd);

                    context.RouteData.Values.Add("jwtToken", val);
                    context.RouteData.Values.Add("password", _pwd);
                }
                else
                    throw new Exception("不包含验证信息");
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers[_keyValuePair.Item1] = _keyValuePair.Item2;
        }


    }
}
