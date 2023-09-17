using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Modals;

namespace ProductApp.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]

        public IActionResult getAll()
        {
            var products = new List<Product>()
            {
                new Product() { Id = 1, ProductName = "Bilgisayar" },
                new Product() { Id = 2, ProductName = "Telefon" }
            };

            _logger.LogInformation("Get All Methodu Calıstı");
            return Ok(products);
        }

        [HttpPost]

        public IActionResult getAllProducts([FromBody]Product product)
        {

            _logger.LogWarning("Product Eklendi");
            return StatusCode(201);
        }

    }
}
