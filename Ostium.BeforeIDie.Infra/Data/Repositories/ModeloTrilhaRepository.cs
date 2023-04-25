using Ostium.BeforeIDie.Domain.Contracts.Repositories;
using Ostium.BeforeIDie.Domain.Contracts.Settings;
using Ostium.BeforeIDie.Domain.Entities;
using Ostium.BeforeIDie.Domain.Model.Contracts.Repositories;
using Ostium.BeforeIDie.Infra.Data.Repositories.Base;

namespace Ostium.BeforeIDie.Infra.Data.Repositories
{
    public class ModeloTrilhaRepository : RepositoryBase<ModeloTrilhaEntity>, IModeloTrilhaRepository
    {

        public ModeloTrilhaRepository(IDatabaseSettings settings) : base(settings)
        {

        }
    }
}

