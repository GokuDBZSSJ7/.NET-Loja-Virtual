using Core.Entities;
using Core.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly AppDbContext _context;

    public OrderItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderItem>> GetAllAsync() =>
        await _context.OrderItems.AsNoTracking().ToListAsync();

    public async Task<OrderItem?> GetByIdAsync(int id) =>
        await _context.OrderItems.FindAsync(id);

    public async Task AddAsync(OrderItem orderItem)
    {
        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(OrderItem orderItem)
    {
        _context.OrderItems.Update(orderItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(OrderItem orderItem)
    {
        _context.OrderItems.Remove(orderItem);
        await _context.SaveChangesAsync();
    }
}