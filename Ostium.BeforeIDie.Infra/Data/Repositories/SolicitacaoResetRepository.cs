using Ostium.BeforeIDie.Domain.Contracts.Repositories;
using Ostium.BeforeIDie.Domain.Contracts.Settings;
using Ostium.BeforeIDie.Domain.Entities;
using Ostium.BeforeIDie.Infra.Data.Repositories.Base;

namespace Ostium.BeforeIDie.Infra.Data.Repositories
{
    public class SolicitacaoResetRepository : RepositoryBase<SolicitacaoResetEntity>, ISolicitacaoResetRepository
    {

        public SolicitacaoResetRepository(IDatabaseSettings settings) : base(settings)
        {

        }
    }
}

