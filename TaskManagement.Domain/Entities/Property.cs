using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskManagement.Domain.Entities
{
    public class Property
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? IdProperty { get; set; }

        public string? Name { get; set; }
        public string? Address { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        public string? CodeInternal { get; set; }
        public int Year { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? IdOwner { get; set; }

        public bool Featured { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}


