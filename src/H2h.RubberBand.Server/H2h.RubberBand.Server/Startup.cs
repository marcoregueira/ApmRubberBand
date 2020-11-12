using H2h.RubberBand.Database.Crud;
using H2h.RubberBand.Database.Database;
using H2h.RubberBand.Database.Postgres;
using H2h.RubberBand.Database.SqlServer.Context;
using H2h.RubberBand.Server.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace H2h.RubberBand.Server
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
            switch (Configuration.GetValue<string>("DatabaseProvider"))
            {
                case "sqlserver":
                    services.AddScoped<BaseContext, SqlServerContext>();
                    break;

                default:
                    services.AddScoped<BaseContext, PostgresContext>();
                    break;
            }

            services.AddTransient<IMetricCrud, MetricCrud>();
            services.AddTransient<IConfigCrud, ConfigCrud>();
            services.AddOptions<ApmOptions>()
                .Bind(Configuration.GetSection("ApmOptions"));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BaseContext dbContext)
        {
            dbContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}