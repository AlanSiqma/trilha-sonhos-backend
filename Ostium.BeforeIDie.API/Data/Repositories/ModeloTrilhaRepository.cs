using Ostium.BeforeIDie.API.Data.Repositories.Base;
using Ostium.BeforeIDie.API.Model.Contracts.Repositories;
using Ostium.BeforeIDie.API.Model.Contracts.Settings;
using Ostium.BeforeIDie.API.Model.Entities;

namespace Ostium.BeforeIDie.API.Data.Repositories
{
    public class ModeloTrilhaRepository : RepositoryBase<ModeloTrilhaEntity>, IModeloTrilhaRepository
    {

        public ModeloTrilhaRepository(IDatabaseSettings settings) : base(settings)
        {

        }
    }
}

