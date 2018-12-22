using Geotik.Entities;
using Geotik.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Geotik
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(options =>
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));

            //add the connections string to environment variables, as it has sensitive data
            string connectionString = Configuration.GetConnectionString("GeotikDbConnectionString");
            services.AddDbContext<GeotikContext>(o => o.UseSqlServer(connectionString));

            services.AddLogging();

            services.AddScoped<IGeotikRepository, GeotikRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            GeotikContext geotikContext)
        {
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            geotikContext.EnsureSeedDataForContext();
            app.UseStatusCodePages();

            app.UseMvc();
        }
    }
}
