using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Model
{
    public class NewOrderData
    {
        public string Address { get; set; }
        public IDictionary<string, double> Items { get; set; }
    }
}
