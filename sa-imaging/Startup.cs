using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using sa_imaging.Services;

[assembly: FunctionsStartup(typeof(MyNamespace.Startup))]

namespace MyNamespace
{
    public class Startup : FunctionsStartup
    {
        //private readonly IServiceCollection serviceMap;
        public Startup()
        {

        }
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
            //this.serviceMap = services;
        }

        public IConfiguration Configuration { get; }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<ICopy>((s) =>
            {
                return new Copy(Configuration);
            });
            builder.Services.AddScoped<ICopy, Copy>();
        }
    }
}
