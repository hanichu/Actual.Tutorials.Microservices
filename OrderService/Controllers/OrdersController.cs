using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using OrderService.Model;
using OrderService.Repositories;
using Rebus.Bus;

namespace OrderService.Controllers
{
    public class OrdersController : ControllerBase
    {
        private string connectionString = "mongodb://localhost:27017/orders-db";

        public IBus Bus { get; set; }

        public OrdersController(IBus bus)
        {
            this.Bus = bus;
        }

        [HttpPost]
        public IActionResult GetAll()
        {
            OrdersRepository repository = new OrdersRepository(connectionString);

            var orders = repository.GetAll();

            return new JsonResult(new SuccessWithResult<IEnumerable<Order>>(orders));
        }

        [HttpPost]
        public IActionResult GetById(string id)
        {
            OrdersRepository repository = new OrdersRepository(connectionString);

            var order = repository.GetById(id);

            if (order == null)
                return new JsonResult(new Fault("Order not found"));

            return new JsonResult(new SuccessWithResult<Order>(order));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Save([FromBody] NewOrderData data)
        {
            if (!data.Items.Any())
                return new JsonResult(new Fault("Order does not have any item"));

            var order = new Order
            {
                Id = ObjectId.GenerateNewId(),
                Address = data.Address,
                Items = from item in data.Items
                        select new OrderItem { SKU = new ObjectId(item.Key), Quantity = item.Value }
            };

            OrdersRepository repository = new OrdersRepository(connectionString);

            repository.SaveOrder(order);

            this.Bus.Publish(new OrderCreatedMessage
            {
                OrderId = order.Id.ToString(),
                Items = data.Items
            });

            return new JsonResult(new SuccessWithResult<string>(order.Id.ToString()));
        }
    }
}
