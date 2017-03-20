using Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Web;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

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
                    PostLogoutRedirectUri = "https://localhost:44320",
                    Notifications = new OpenIdConnectAuthenticationNotifications()
                    {
                        AuthorizationCodeReceived = (context) =>
                        {
                            Debug.WriteLine("*** Auth code received");
                            string ClientId = "b8cd2a65-ab87-430b-9271-778778e1c50f";
                            string Authority = "https://login.microsoftonline.com/microsoft492.onmicrosoft.com";
                            string appKey = "OW3BzKvXdry/A6FOrtrPltjW2ZRebnniaVXIppRKWZw=";
                            string resourceId = "https://graph.windows.net";
                            var code = context.Code;
                            AuthenticationContext authContext = new AuthenticationContext(Authority);
                            ClientCredential credential = new ClientCredential(ClientId, appKey);
                            AuthenticationResult result = authContext.AcquireTokenByAuthorizationCode(code,
                                new System.Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path)),
                                credential,
                                resourceId);

                            //string callOutcome = string.Empty;
                            //HttpClient httpClient = new HttpClient();
                            //httpClient.DefaultRequestHeaders.Authorization = 
                            //    new AuthenticationHeaderValue("Bearer", result.AccessToken);
                            //HttpResponseMessage response = 
                            //    httpClient.GetAsync("https://graph.windows.net/me?api-version=1.6").Result;
                            //if (response.IsSuccessStatusCode)
                            //{
                            //    callOutcome = response.Content.ReadAsStringAsync().Result;
                            //}




                            return Task.FromResult(0);
                        }
                    }
                }
            );
        }
    }
}