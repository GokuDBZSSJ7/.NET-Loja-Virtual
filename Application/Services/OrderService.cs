using Core.Entities;
using Core.Interface;
using Shared.Exceptions;

namespace Application.Services;

public class OrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Order>> GetAllAsync() =>
        await _repository.GetAllAsync();

    public async Task<Order?> GetByIdAsync(int id)
    {
        var order = await _repository.GetByIdAsync(id);
        return order ?? throw new NotFoundException($"Order with ID {id} not found");
    }

    public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId)
    {
        return await _repository.GetByCustomerIdAsync(customerId);
    }

    public async Task AddAsync(Order order) =>
        await _repository.AddAsync(order);

    public async Task UpdateAsync(Order order)
    {
        var existing = await _repository.GetByIdAsync(order.Id);
        if (existing == null) throw new NotFoundException($"Order with ID {order.Id} not found");

        await _repository.UpdateAsync(order);
    }

    public async Task DeleteAsync(Order order)
    {
        var existing = await _repository.GetByIdAsync(order.Id);
        if (existing == null) throw new NotFoundException($"Order with ID {order.Id} not found");

        await _repository.DeleteAsync(order);
    }
    
}