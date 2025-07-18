namespace Core.Entities;

public class Coupon : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public bool IsPercentage { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int? MaxUsages { get; set; }
    public int TimesUsed { get; set; }
    public decimal? MinimumOrderAmount { get; set; }
}