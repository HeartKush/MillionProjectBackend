using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskManagement.Domain.Entities
{
    public class PropertyTrace
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? IdPropertyTrace { get; set; }

        public DateTime DateSale { get; set; }
        public string? Name { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Value { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Tax { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? IdProperty { get; set; }
    }
}


