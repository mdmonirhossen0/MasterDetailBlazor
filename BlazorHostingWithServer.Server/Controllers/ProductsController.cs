using BlazorHostingWithServer.Server.Repository;
using BlazorHostingWithServer.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorHostingWithServer.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<List<Product>> GetProductList()
        {
            return await _productService.GetProducts();
        }
        [HttpGet("{id}")]
        public async Task<Product?> GetProductById(int id)
        {
            return await _productService.GetProductById(id);
        }
        [HttpPost]
        public async Task<Product> CreateNewProduct(Product product)
        {
            return await _productService.CreateProduct(product);
        }
        [HttpPut("{id}")]
        public async Task<Product?> UpdateProduct(int id, Product product)
        {
            return await _productService.UpdateProduct(id, product);
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteProduct(int id)
        {
            return await _productService.DeleteProduct(id);
        }
    }
}
