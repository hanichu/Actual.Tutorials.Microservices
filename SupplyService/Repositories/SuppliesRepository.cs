using MongoDB.Bson;
using MongoDB.Driver;
using SupplyService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Repositories
{
    public class SuppliesRepository
    {
        public IMongoDatabase Database { get; }

        public SuppliesRepository(string connectionString)
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

        internal void DecreaseAvailability(string key, double value)
        {
            var collection = this.Database.GetCollection<ProductData>("products");

            var id = new ObjectId(key);

            ProductData product = collection.Find<ProductData>(o => o.Id == id).FirstOrDefault();

            if (product != null)
            {
                collection.UpdateOne<ProductData>(
                    o => o.Id == id,
                    Builders<ProductData>.Update.Set("availability", product.Availability - value));
            }
        }

        internal ProductData GetById(string id)
        {
            var collection = this.Database.GetCollection<ProductData>("products");
            return collection.Find<ProductData>(o => o.Id == new MongoDB.Bson.ObjectId(id)).FirstOrDefault();
        }

        internal IEnumerable<ProductData> GetAll()
        {
            var collection = this.Database.GetCollection<ProductData>("products");
            return collection.Find<ProductData>(o => true).ToList();
        }


        public void SaveProduct(ProductData product)
        {
            var collection = this.Database.GetCollection<ProductData>("products");
            collection.InsertOne(product);
        }
    }
}
