using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _catalogContext;

    public ProductRepository(ICatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task CreateProduct(Product product)
    {
        await _catalogContext
            .Products
            .InsertOneAsync(product);
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var result = await _catalogContext
            .Products
            .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

        return result.IsAcknowledged
            && result.ModifiedCount is 1;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

        DeleteResult deleteResult = await _catalogContext
            .Products
            .DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged
            && deleteResult.DeletedCount is 1;
    }

    public async Task<Product> GetProduct(string id)
    {
        return await _catalogContext
            .Products
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

        return await _catalogContext
            .Products
            .Find(filter)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

        return await _catalogContext
            .Products
            .Find(filter)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _catalogContext
            .Products
            .Find(p => true)
            .ToListAsync();
    }
}
