using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Repositories
{
    public class Order
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("address")]
        public string Address { get; set; }
        [BsonElement("items")]
        public IEnumerable<OrderItem> Items { get; set; }
    }

    public class OrderItem
    {
        [BsonElement("sku")]
        public ObjectId SKU { get; set; }
        [BsonElement("quantity")]
        public double Quantity { get; set; }
    }
}


