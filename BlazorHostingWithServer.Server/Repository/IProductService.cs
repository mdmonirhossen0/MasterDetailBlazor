using BlazorHostingWithServer.Shared;

namespace BlazorHostingWithServer.Server.Repository
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task<Product?> GetProductById(int id);
        Task<Product> CreateProduct(Product product);
        Task<Product?> UpdateProduct(int productId,Product product);
        Task<bool> DeleteProduct(int productId);

    }
}
