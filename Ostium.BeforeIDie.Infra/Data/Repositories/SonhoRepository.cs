using Ostium.BeforeIDie.Domain.Contracts.Respositories;
using Ostium.BeforeIDie.Domain.Contracts.Settings;
using Ostium.BeforeIDie.Domain.Entities;
using Ostium.BeforeIDie.Infra.Data.Repositories.Base;

namespace Ostium.BeforeIDie.Infra.Data.Repositories
{
    public class SonhoRepository : RepositoryBase<SonhoEntity>, ISonhoRepository
    {

        public SonhoRepository(IDatabaseSettings settings) : base(settings)
        {

        }
    }
}

