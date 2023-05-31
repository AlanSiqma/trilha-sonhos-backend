using Microsoft.Extensions.DependencyInjection;
using Ostium.BeforeIDie.Domain.Contracts.Repositories;
using Ostium.BeforeIDie.Domain.Contracts.Respositories;
using Ostium.BeforeIDie.Domain.Contracts.Services;
using Ostium.BeforeIDie.Domain.Model.Contracts.Repositories;
using Ostium.BeforeIDie.Infra.Data.Repositories;
using Ostium.BeforeIDie.Services;
using System.Diagnostics.CodeAnalysis;

namespace Ostium.BeforeIDie.IoC
{
    [ExcludeFromCodeCoverageAttribute]
    public static class InjectionConfigurations
    {
        public static IServiceCollection AddInjection(this IServiceCollection services)
        {
            services.AddScoped<ISonhadorRepository, SonhadorRepository>();
            services.AddScoped<ISonhoRepository, SonhoRepository>();
            services.AddScoped<IModeloTrilhaRepository, ModeloTrilhaRepository>();
            services.AddScoped<ISolicitacaoResetRepository, SolicitacaoResetRepository>();
        
            services.AddScoped<IEmailService, EmailSender>();
            services.AddScoped<ISonhadorService, SonhadorService>();
            
            return services;
        }
    }
}
