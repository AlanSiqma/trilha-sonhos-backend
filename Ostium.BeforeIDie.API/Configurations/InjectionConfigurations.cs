using Microsoft.Extensions.DependencyInjection;
using Ostium.BeforeIDie.API.Data.Repositories;
using Ostium.BeforeIDie.API.Model.Contracts.Repositories;
using Ostium.BeforeIDie.API.Model.Contracts.Respositories;
using ToolBoxDeveloper.TemplateEmail.Package.Contracts;
using ToolBoxDeveloper.TemplateEmail.Package.Proxies;

namespace Ostium.BeforeIDie.API.Configurations
{
    public static class InjectionConfigurations
    {
        public static IServiceCollection AddInjection(this IServiceCollection services)
        {
            services.AddScoped<ISonhadorRepository, SonhadorRepository>();
            services.AddScoped<ISonhoRepository, SonhoRepository>();
            services.AddScoped<IModeloTrilhaRepository, ModeloTrilhaRepository>();
            services.AddScoped<ISolicitacaoResetRepository, SolicitacaoResetRepository>();
            services.AddScoped<IEmailProxy, EmailProxy>();

            return services;
        }
    }
}
