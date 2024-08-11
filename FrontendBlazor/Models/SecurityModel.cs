using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace FrontendBlazor.Models
{
    public class SecurityModel : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("Login") == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller","Registration" },
                    { "action","Login" }
                });
            }

            var token = context.HttpContext.Session.GetString("Token");

            if (token == null)
            {
                // Si no hay token, redirige al usuario al inicio de sesión
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
            {
                { "controller","Registration" },
                { "action","Login" }
            });
                return;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            // Verifica si el token ha expirado
            if (jwtToken.ValidTo < DateTime.UtcNow)
            {
                // Si el token ha expirado, redirige al usuario al inicio de sesión
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
            {
                { "controller","Registration" },
                { "action","Login" }
            });
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
