using Ostium.BeforeIDie.API.Model.Entities.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ostium.BeforeIDie.API.Model.Contracts.Repositories.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> Get();
        Task<TEntity> Get(string id);
        Task<TEntity> Create(TEntity entity);
        Task Update(string id, TEntity entity);
        Task Remove(TEntity entity);
        Task Remove(string id);
    }
}
