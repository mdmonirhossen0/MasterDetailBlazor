using BlazorHostingWithServer.Server.Data;
using BlazorHostingWithServer.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorHostingWithServer.Server.Repository
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;

        public ProductService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<Product> CreateProduct(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }
        public async Task<bool> DeleteProduct(int productId)
        {
            var delProduct = await _db.Products.FindAsync(productId);
            if (delProduct == null)
            {
                return false;
            }
            _db.Products.Remove(delProduct);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<Product?> GetProductById(int id)
        {
            var existingProduct = await _db.Products.FindAsync(id);
            return existingProduct;
        }
        public async Task<List<Product>> GetProducts()
        {
            return await _db.Products.ToListAsync();
        }
        public async Task<Product?> UpdateProduct(int productId, Product product)
        {
            var existingProduct = await _db.Products.FindAsync(productId);
            if (existingProduct != null)
            {
                existingProduct.Title=product.Title;
                existingProduct.Category=product.Category;
                existingProduct.Price=product.Price;
                await _db.SaveChangesAsync();
            }
            return existingProduct;
        }
    }
}
