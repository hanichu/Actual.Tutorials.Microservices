using Common;
using OrderService.Repositories;
using Rebus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplyService.Handlers
{
    public class MessageHandler
        : IHandleMessages<OrderCreatedMessage>
    {
        public async Task Handle(OrderCreatedMessage message)
        {
            SuppliesRepository repository = new SuppliesRepository(Program.connectionString);

            foreach (var item in message.Items)
                repository.DecreaseAvailability(item.Key, item.Value);

            await Task.Yield();
        }
    }
}
