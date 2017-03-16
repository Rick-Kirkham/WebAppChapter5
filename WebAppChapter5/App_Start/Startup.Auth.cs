using Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;

namespace WebAppChapter5
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = "b8cd2a65-ab87-430b-9271-778778e1c50f",
                    Authority = "https://login.microsoftonline.com/microsoft492.onmicrosoft.com",
                    PostLogoutRedirectUri = "https://localhost:44320"
                }
            );
        }
    }
}