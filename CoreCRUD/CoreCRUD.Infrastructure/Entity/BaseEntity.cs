using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CoreCRUD.Infrastructure.Entity
{
    /// <summary>
    /// Classe base para as entidades
    /// </summary>
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
