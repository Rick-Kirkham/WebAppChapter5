using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace WebAppChapter5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            string ClientId = "b8cd2a65-ab87-430b-9271-778778e1c50f";
            string Authority = "https://login.microsoftonline.com/microsoft492.onmicrosoft.com";
            string appKey = "OW3BzKvXdry/A6FOrtrPltjW2ZRebnniaVXIppRKWZw=";
            string resourceId = "https://graph.windows.net";

            
            ClientCredential credential = new ClientCredential(ClientId, appKey);
            AuthenticationContext authContext = new AuthenticationContext(Authority);

            AuthenticationResult result = authContext.AcquireTokenSilent(resourceId,credential,UserIdentifier.AnyUser);

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", result.AccessToken);
            HttpResponseMessage response =
                httpClient.GetAsync("https://graph.windows.net/me?api-version=1.6").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            }

            
            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            string userfirstname = ClaimsPrincipal.Current.FindFirst(ClaimTypes.GivenName).Value;
            ViewBag.Message = String.Format("Welcome, {0}!", userfirstname);

            return View();
        }


    }
}