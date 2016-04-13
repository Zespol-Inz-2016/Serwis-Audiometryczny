using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SerwisAudiometryczny.Startup))]
namespace SerwisAudiometryczny
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
