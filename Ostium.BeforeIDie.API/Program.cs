using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Ostium.BeforeIDie.API.Extentions;
using System;

namespace Ostium.BeforeIDie.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var ASPNETCORE_ENVIRONMENT =
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (ASPNETCORE_ENVIRONMENT == null)
                DotEnvExtention.Load();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}