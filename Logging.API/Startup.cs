using Logging.Infrastructure.Filters;
using Logging.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Logging.API
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
            services.AddControllers(options =>
            {
                //Add global filter of certain type
                options.Filters.Add<TrackActionPerformanceFilter>();
            });

            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //Create some exception handling middleware
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //Add the new middleware
            //app.UseApiExceptionHandler();

            //Add the new middleware with options
            app.UseApiExceptionHandler(options =>
            {
                options.AddResponseDetails = (context, exception, error) =>
                {
                    //Can be defined here or separately
                    error.Detail = $"Custom error detail--Exception type { exception.GetType().Name}";
                };
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
