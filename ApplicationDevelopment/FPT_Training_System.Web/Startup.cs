using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;


[assembly: OwinStartup(typeof(FPT_Training_System.Web.Startup))]

namespace FPT_Training_System.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            ConfigureAuth(app);
        }
    }
}
