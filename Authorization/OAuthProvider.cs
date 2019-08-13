using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using Authorization.Models;
using System.Collections.Generic;
using System.Security.Claims;
using Authorization;

namespace Authorization
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                string username = context.UserName;
                string password = context.Password;

                SRJUsuario user = new SRJUsuario().Get(username, password);
                if (user != null)
                {
                    List<Claim> clains = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Nome),
                        new Claim("UserID", user.id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role)
                    };

                    ClaimsIdentity OAuthIdentity = new ClaimsIdentity(clains, Startup.OAuthOptions.AuthenticationType);
                    context.Validated(
                           new Microsoft.Owin.Security.AuthenticationTicket(
                               OAuthIdentity,
                               new Microsoft.Owin.Security.AuthenticationProperties() { }));
                }
                else
                {
                    context.SetError("Erro", "Erro");
                }
            });
        }


        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }
    }
}