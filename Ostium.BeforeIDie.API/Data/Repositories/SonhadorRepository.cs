using Ostium.BeforeIDie.API.Data.Repositories.Base;
using Ostium.BeforeIDie.API.Model.Contracts.Respositories;
using Ostium.BeforeIDie.API.Model.Contracts.Settings;
using Ostium.BeforeIDie.API.Model.Dto;
using Ostium.BeforeIDie.API.Model.Entities;
using System.Threading.Tasks;

namespace Ostium.BeforeIDie.API.Data.Repositories
{
    public class SonhadorRepository: RepositoryBase<SonhadorEntity>, ISonhadorRepository
    {
        public SonhadorRepository(IDatabaseSettings settings):base(settings)
        {


        }
    }
}