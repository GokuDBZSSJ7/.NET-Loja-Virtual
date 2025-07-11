using Core.Entities;
using Core.Interface;
using Shared.Exceptions;

namespace Application.Services;

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Product>> GetAllAsync() => await _repository.GetAllAsync();

    public async Task<Product> GetByIdAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        return product ?? throw new NotFoundException($"Product with ID {id} not found");
    }

    public async Task AddAsync(Product product) => await _repository.AddAsync(product);

    public async Task UpdateAsync(Product product)
    {
        var existing = await _repository.GetByIdAsync(product.Id);
        if (existing == null) throw new NotFoundException($"Product with ID {product.Id} not found");

        existing.Name = product.Name;
        existing.Description = product.Description;
        existing.Price = product.Price;
        existing.Stock = product.Stock;
        existing.CategoryId = product.CategoryId;

        await _repository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null) throw new NotFoundException($"Product with ID {id} not found");

        await _repository.DeleteAsync(existing);
    }
}