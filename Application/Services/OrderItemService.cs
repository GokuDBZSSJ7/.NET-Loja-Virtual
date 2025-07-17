using Core.Entities;
using Core.Interface;
using Shared.Exceptions;

namespace Application.Services;

public class OrderItemService
{
    private readonly IOrderItemRepository _repository;

    public OrderItemService(IOrderItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<OrderItem>> GetAllAsync() =>
        await _repository.GetAllAsync();

    public async Task<OrderItem?> GetByIdAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        return item ?? throw new NotFoundException($"Ordem Item with ID {id} not found");
    }

    public async Task AddAsync(OrderItem orderItem)
    {
        await _repository.AddAsync(orderItem);
    }

    public async Task UpdateAsync(OrderItem orderItem)
    {
        var existing = await _repository.GetByIdAsync(orderItem.Id);
        if (existing == null) throw new NotFoundException($"Order Item with ID {orderItem.Id} not found");

        await _repository.UpdateAsync(orderItem);
    }

    public async Task DeleteAsync(OrderItem orderItem)
    {
        var existing = await _repository.GetByIdAsync(orderItem.Id);
        if (existing == null) throw new NotFoundException($"Order Item with ID {orderItem.Id} not found");

        await _repository.DeleteAsync(orderItem);
    }
}