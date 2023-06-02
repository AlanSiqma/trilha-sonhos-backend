using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace Ostium.BeforeIDie.Domain.Entities.Base
{
    [ExcludeFromCodeCoverageAttribute]
    public abstract class BaseEntity{

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
      }
}