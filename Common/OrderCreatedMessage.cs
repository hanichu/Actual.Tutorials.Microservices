using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class OrderCreatedMessage
    {
        public string OrderId { get; set; }

        public IDictionary<string, double> Items { get; set; }
    }
}
