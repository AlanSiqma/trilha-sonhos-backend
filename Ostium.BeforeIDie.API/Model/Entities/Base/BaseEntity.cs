using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ostium.BeforeIDie.API.Model.Entities.Base
{
     public abstract class BaseEntity{

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
      }
}