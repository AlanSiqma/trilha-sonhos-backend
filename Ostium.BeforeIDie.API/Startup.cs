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

namespace Ostium.BeforeIDie.API
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

            services.AddCors();

            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowOrigin", options => options.WithOrigins("https://localhost:4200"));
            //    c.AddPolicy("AllowOrigin", options => options.WithOrigins("http://26.27.214.133"));
            //    c.AddPolicy("AllowOrigin", options => options.WithOrigins("http://DESKTOP-RDTVBO4"));
            //});

            services.Configure<DatabaseSettings>(
                        Configuration.GetSection(nameof(DatabaseSettings))
                );

            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ostium.BeforeIDie.API", Version = "v1" });
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddInjection();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ostium.BeforeIDie.API v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

            app.UseCors("AllowOrigin");

            app.UseAuthorization();

            //app.UseCors(options => options.WithOrigins("https://localhost:4200"));

            //app.UseCors(options => options.WithOrigins("http://26.27.214.133"));

            //app.UseCors(options => options.WithOrigins("http://DESKTOP-RDTVBO4"));


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
