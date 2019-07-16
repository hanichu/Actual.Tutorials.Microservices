using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Mvc;
using OrderService.Repositories;
using SupplyService.Model;

namespace SupplyService.Controllers
{
    public class SuppliesController : ControllerBase
    {
        [HttpPost]
        public IActionResult GetProductBySKU(string sku)
        {
            SuppliesRepository repository = new SuppliesRepository(Program.connectionString);

            var product = repository.GetById(sku);

            if (product == null)
                return new JsonResult(new Fault("Product does not exists"));

            return new JsonResult(new SuccessWithResult<ProductData>(product));
        }

        [HttpPost]
        public IActionResult GetAll(int sku)
        {
            SuppliesRepository repository = new SuppliesRepository(Program.connectionString);

            var products = repository.GetAll();

            return new JsonResult(new SuccessWithResult<IEnumerable<ProductData>>(products));
        }
    }
}
