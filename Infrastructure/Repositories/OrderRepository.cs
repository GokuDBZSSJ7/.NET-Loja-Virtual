using Core.Entities;
using Core.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllAsync() =>
        await _context.Orders.AsNoTracking().ToListAsync();

    public async Task<Order?> GetByIdAsync(int id) =>
        await _context.Orders.FindAsync(id);

    public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId) =>
        await _context.Orders
            .Where(o => o.CustomerId == customerId)
            .AsNoTracking()
            .ToListAsync();

    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Order order)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }

}