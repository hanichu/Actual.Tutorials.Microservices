using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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

        [HttpPost]
        public IActionResult Save([FromBody] ProductData data)
        {
            if (data.Availability<=0)
                return new JsonResult(new Fault("Product not available"));

            var product = new ProductData()
            {
                Id = ObjectId.GenerateNewId(),
                Availability = data.Availability,
                Categories = data.Categories,
                Description = data.Description
            };

            SuppliesRepository repository = new SuppliesRepository(Program.connectionString);

            repository.SaveProduct(product);

            /*
            Stampa al posto dell'id il DTO con tanto di uri del prodotto 
            */
            var productUri = $"http://{{host}}/supplies/GetProductBySKU?sku={product.Id.ToString()}";
            
            return new JsonResult(new SuccessWithResult<string>(productUri));
        }

    }
}
