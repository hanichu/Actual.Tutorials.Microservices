using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplyService.Model
{
    public class ProductDto
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("availability")]
        public double Availability { get; set; }
        [BsonElement("categories")]
        public IEnumerable<string> Categories { get; set; }

        public string Dto { get; set; }
    }
}
