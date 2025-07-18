using Core.Entities;
using Core.Interface;
using Shared.Exceptions;

namespace Application.Services;

public class CouponService
{
    private readonly ICouponRepository _repository;

    public CouponService(ICouponRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Coupon>> GetAllAsync()
        => await _repository.GetAllAsync();

    public async Task<Coupon> GetByIdAsync(int id)
    {
        var coupon = await _repository.GetByIdAsync(id);
        return coupon ?? throw new NotFoundException($"Cupom com ID {id} não encontrado");
    }

    public async Task<Coupon> GetByCodeAsync(string code)
    {
        var coupon = await _repository.GetByCodeAsync(code);
        return coupon ?? throw new NotFoundException($"Cupom com código '{code}' não encontrado");
    }

    public async Task AddAsync(Coupon coupon)
    {
        var existing = await _repository.GetByCodeAsync(coupon.Code);
        if (existing is not null)
            throw new ConflictException($"Já existe um cupom com o código '{coupon.Code}'");

        coupon.TimesUsed = 0; 
        await _repository.AddAsync(coupon);
    }

    public async Task UpdateAsync(Coupon coupon)
    {
        var existing = await _repository.GetByIdAsync(coupon.Id);
        if (existing == null)
            throw new NotFoundException($"Cupom com ID {coupon.Id} não encontrado");

        existing.Code = coupon.Code;
        existing.Value = coupon.Value;
        existing.IsPercentage = coupon.IsPercentage;
        existing.ExpirationDate = coupon.ExpirationDate;
        existing.MaxUsages = coupon.MaxUsages;
        existing.MinimumOrderAmount = coupon.MinimumOrderAmount;

        await _repository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new NotFoundException($"Cupom com ID {id} não encontrado");

        await _repository.DeleteAsync(existing);
    }

    public async Task<decimal> CalculateDiscountAsync(string code, decimal orderTotal)
    {
        var coupon = await GetByCodeAsync(code);

        if (coupon.ExpirationDate < DateTime.UtcNow)
            throw new ConflictException("Cupom expirado.");

        if (coupon.MaxUsages.HasValue && coupon.TimesUsed >= coupon.MaxUsages.Value)
            throw new ConflictException("Este cupom já atingiu o número máximo de usos.");

        if (coupon.MinimumOrderAmount.HasValue && orderTotal < coupon.MinimumOrderAmount.Value)
            throw new ConflictException($"Valor mínimo do pedido para este cupom é R$ {coupon.MinimumOrderAmount.Value:N2}");

        decimal discount = coupon.IsPercentage
            ? orderTotal * (coupon.Value / 100m)
            : coupon.Value;

        return discount;
    }

    public async Task IncrementUsageAsync(string code)
    {
        var coupon = await GetByCodeAsync(code);
        coupon.TimesUsed++;
        await _repository.UpdateAsync(coupon);
    }
}
