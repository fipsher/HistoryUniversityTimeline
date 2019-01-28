using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HU.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class HUAttribute : TypeFilterAttribute
    {
        public HUAttribute() : base(typeof(HUFilter))
        {

        }
    }

    public class HUFilter : IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var tokenName = context.HttpContext.Request.Cookies["Token"];
            if (String.IsNullOrEmpty(tokenName) || !String.Equals(tokenName, AppSettings.TokenKey, StringComparison.InvariantCultureIgnoreCase))
            {
                context.Result = new RedirectResult("/api/Admin/Authenticate");
            }

        }
    }

}