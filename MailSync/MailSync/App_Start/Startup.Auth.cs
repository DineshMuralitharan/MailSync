using System.Configuration;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

namespace MailSync
{ 
    public partial class Startup
    { 
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
         
        private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
          
        private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
         
        private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];
         
        private static string authority = string.Format(CultureInfo.InvariantCulture, aadInstance, tenant);

        /// <summary>
        /// configure cookies
        /// </summary>
        /// <param name="app">app builder value</param>
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseKentorOwinCookieSaver();
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = clientId,
                    Authority = authority,
                    PostLogoutRedirectUri = postLogoutRedirectUri,
                    RedirectUri = postLogoutRedirectUri,
                    UseTokenLifetime = false,
                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        AuthenticationFailed = context =>
                        {
                            if (context.Exception.Message.StartsWith("OICE_20004") || context.Exception.Message.Contains("IDX10311"))
                            {
                                context.SkipToNextMiddleware();
                                context.Response.Redirect("/");
                                return Task.FromResult(0);
                            }

                            return Task.FromResult(0);
                        }
                    }
                });
        }
    }
}