using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskManagement.Domain.Entities
{
    public class PropertyImage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? IdPropertyImage { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? IdProperty { get; set; }

        public string? File { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}


