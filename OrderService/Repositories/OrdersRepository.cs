using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Repositories
{
    public class OrdersRepository
    {
        public IMongoDatabase Database { get; }

        public OrdersRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            var client = new MongoClient(connectionString);

            var databaseName = new MongoUrl(connectionString).DatabaseName;

            if (string.IsNullOrEmpty(databaseName))
                throw new Exception($"Invalid connection string: missing database name");

            this.Database = client.GetDatabase(databaseName);
        }

        internal Order GetById(string id)
        {
            var collection = this.Database.GetCollection<Order>("orders");
            return collection.Find<Order>(o => o.Id == new MongoDB.Bson.ObjectId(id)).FirstOrDefault();
        }

        internal IEnumerable<Order> GetAll()
        {
            var collection = this.Database.GetCollection<Order>("orders");
            return collection.Find<Order>(o => true).ToList();
        }

        public void SaveOrder(Order order)
        {
            var collection = this.Database.GetCollection<Order>("orders");
            collection.InsertOne(order);
        }
    }
}
