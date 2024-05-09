using BlazorHostingWithServer.Shared;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace BlazorHostingWithServer.Client.Repository
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _manager;

        public ProductService(HttpClient http, NavigationManager manager)
        {
            _http = http;
            _manager = manager;
        }

        public async Task CreateProduct(Product product)
        {
            await _http.PostAsJsonAsync("api/products", product);
            _manager.NavigateTo("products");
        }

        public async Task DeleteProduct(int id)
        {
            var result = await _http.DeleteAsync($"api/products/{id}");
            _manager.NavigateTo("products");
        }

        public async Task<Product?> GetProductById(int id)
        {
            var result = await _http.GetAsync($"api/products/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<Product>();
            }
            return null;
        }
        public List<Product> Products { get; set; } = new List<Product>();
       

        public async Task GetProducts()
        {
            var result = await _http.GetFromJsonAsync<List<Product>>("api/products");
            if (result != null)
            {
                Products = result.ToList();
            }
        }

        async Task IProductService.UpdateProduct(int productId, Product product)
        {
            await _http.PutAsJsonAsync($"api/products/{productId}", product);
            _manager.NavigateTo("products");
        }
    }
}
