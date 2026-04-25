namespace Common.Models;

public class Coupon
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public decimal DiscountAmount { get; set; }
    public bool IsUsed { get; set; } = false;
}