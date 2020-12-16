using Ostium.BeforeIDie.API.Data.Repositories.Base;
using Ostium.BeforeIDie.API.Model.Contracts.Respositories;
using Ostium.BeforeIDie.API.Model.Contracts.Settings;
using Ostium.BeforeIDie.API.Model.Entities;

namespace Ostium.BeforeIDie.API.Data.Repositories

{
    public class StatusSonhoRepository : RepositoryBase<StatusSonhoEntity>, IStatusSonhoRepository
    {
        public StatusSonhoRepository(IDatabaseSettings settings) : base(settings)
        {

        }

    }

}