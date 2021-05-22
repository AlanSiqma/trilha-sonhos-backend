using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Ostium.BeforeIDie.API.Configurations;
using Ostium.BeforeIDie.API.Model.Contracts.Settings;
using Ostium.BeforeIDie.API.Settings;
using ToolBoxDeveloper.TemplateEmail.Package.Settings;

namespace Ostium.BeforeIDie.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();

            services.Configure<DatabaseSettings>(
                        Configuration.GetSection(nameof(DatabaseSettings)));

            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);


            services.Configure<EmailProxySetting>(
                    Configuration.GetSection(nameof(EmailProxySetting)));

            services.AddSingleton<EmailProxySetting>(sp =>
               sp.GetRequiredService<IOptions<EmailProxySetting>>().Value);



            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ostium.BeforeIDie.API", Version = "v1" });
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddInjection();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ostium.BeforeIDie.API v1"));

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

            app.UseCors("AllowOrigin");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
