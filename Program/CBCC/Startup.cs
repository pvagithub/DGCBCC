using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CBCC.Startup))]
namespace CBCC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
