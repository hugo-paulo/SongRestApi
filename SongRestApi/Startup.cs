using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SongRestApi.DAL;
using SongRestApi.DAL.Data.Repository;
using SongRestApi.DAL.Data.Repository.IRepository;
using SongRestApi.Installers;
using Swashbuckle.AspNetCore.Swagger;

namespace SongRestApi
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
            /**
             * normally list all the services in this method, but to keep code clean and modular 
             * extract this into seperate classes, see Installer folder
             * **/

            //This is calling the method we defined in InstallerExtension class
            services.InstallServicesInAssembly(Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //The Advanced use of swagger uses the appsetting.json file and specific model for setting options
            var swaggerOptions = new Options.SwaggerOptions();
            //The GetSection is the name in the appsettings.json file, thus binding this with our instance eg above
            Configuration.GetSection(nameof(swaggerOptions)).Bind(swaggerOptions); //advanced

            app.UseSwagger(options =>
            {
                //The swagger end point
                options.RouteTemplate = swaggerOptions.JsonRoute;
            }); //advanced
            /*app.UseSwagger();*/ //basic

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
            }); //advanced
            //app.UseSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Our Song API");
            //});//basic

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
