using AKTest.Business.Services;
using AKTest.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AKTest.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers();

            //injecting services...
            InjectServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();
         
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #region "Service Injector"
        private void InjectServices(IServiceCollection _injectServices)
        {
            _injectServices.AddScoped<IEntrantRepository, EntrantRepository>();
            _injectServices.AddScoped<IEntrantService, EntrantService>();
        }
        #endregion
    }
}
