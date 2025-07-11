using Core.Entities;
using Core.Interface;
using Shared.Exceptions;
using Microsoft.AspNetCore.Identity;


namespace Application.Services;

public class CustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly PasswordHasher<Customer> _passwordHasher;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
        _passwordHasher = new PasswordHasher<Customer>();
    }

    public async Task<IEnumerable<Customer>> GetAllAsync() => await _repository.GetAllAsync();

    public async Task<Customer> GetByIdAsync(int id)
    {
        var customer = await _repository.GetByIdAsync(id);
        return customer ?? throw new NotFoundException($"Customer with ID {id} not found");
    }

    public async Task<Customer> GetByEmailAsync(string email)
    {
        var customer = await _repository.GetByEmailAsync(email);
        return customer ?? throw new NotFoundException($"Customer with email '{email}' not found");
    }

    public async Task AddAsync(Customer customer)
    {
        var existing = await _repository.GetByEmailAsync(customer.Email);
        if (existing is not null)
            throw new ConflictException($"A customer with email '{customer.Email}' already exists");

        customer.PasswordHash = _passwordHasher.HashPassword(customer, customer.PasswordHash);

        await _repository.AddAsync(customer);
    }

    public async Task UpdateAsync(Customer customer)
    {
        var existing = await _repository.GetByIdAsync(customer.Id);
        if (existing == null)
            throw new NotFoundException($"Customer with ID {customer.Id} not found");

        existing.Name = customer.Name;
        existing.Email = customer.Email;
        existing.PasswordHash = customer.PasswordHash;
        existing.Phone = customer.Phone;
        existing.ZipCode = customer.ZipCode;
        existing.Address = customer.Address;
        existing.Type = customer.Type;

        await _repository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new NotFoundException($"Customer with ID {id} not found");

        await _repository.DeleteAsync(existing);
    }
}
