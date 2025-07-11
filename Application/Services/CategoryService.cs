using Core.Entities;
using Core.Interface;
using Shared.Exceptions;

namespace Application.Services;

public class CategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Category>> GetAllAsync() =>
        await _repository.GetAllAsync();

    public async Task<Category> GetByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        return category ?? throw new NotFoundException($"Category with ID {id} not found");
    }

    public async Task AddAsync(Category category) =>
        await _repository.AddAsync(category);

    public async Task UpdateAsync(Category category)
    {
        var existing = await _repository.GetByIdAsync(category.Id);
        if (existing == null) throw new NotFoundException($"Category with ID {category.Id} not found");

        existing.Name = category.Name;

        await _repository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null) throw new NotFoundException($"Category with ID {id} not found");

        await _repository.DeleteAsync(existing);
    }
}