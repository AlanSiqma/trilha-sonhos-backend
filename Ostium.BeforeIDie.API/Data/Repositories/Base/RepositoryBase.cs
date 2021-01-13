using MongoDB.Driver;
using Ostium.BeforeIDie.API.Model.Contracts.Repositories.Base;
using Ostium.BeforeIDie.API.Model.Contracts.Settings;
using Ostium.BeforeIDie.API.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ostium.BeforeIDie.API.Data.Repositories.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        private readonly IMongoCollection<TEntity> _collections;
        public RepositoryBase(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            var databaseName = Activator.CreateInstance(typeof(TEntity)).ToString();

            this._collections = database.GetCollection<TEntity>(databaseName);
        }

        public async Task<List<TEntity>> Get()
        {
            var list = await this._collections.FindAsync(x => true);
            return list.ToList();
        }

        public async Task<TEntity> Get( Expression<Func<TEntity, bool>> predicate )
        {
            var entity = await this._collections.FindAsync<TEntity>(predicate);
            return entity.FirstOrDefault();
        }

        public async Task<TEntity> Get(string id)
        {
            var entity = await this._collections.FindAsync<TEntity>(x => x.Id == id);
            return entity.FirstOrDefault();
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            await _collections.InsertOneAsync(entity);
            return entity;
        }

        public async Task Update(string id, TEntity entity)
        {
            await _collections.ReplaceOneAsync(x => x.Id == id, entity);
        }

        public async Task Remove(TEntity entity)
        {
            await _collections.DeleteOneAsync(x => x.Id == entity.Id);
        }

        public async Task Remove(string id)
        {
            await _collections.DeleteOneAsync(x => x.Id == id);
        }
    }
}
