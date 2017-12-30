using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MailSync.Startup))]
namespace MailSync
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
