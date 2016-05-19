using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PaymentGateway.Web.Startup))]
namespace PaymentGateway.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // ConfigureAuth(app);
        }
    }
}
