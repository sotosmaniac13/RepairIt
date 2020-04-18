using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RepairIt.Startup))]
namespace RepairIt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
