using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoanCompareSite.Startup))]
namespace LoanCompareSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
