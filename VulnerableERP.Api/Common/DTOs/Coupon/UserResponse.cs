namespace Common.DTOs;

public class ApplyCouponRequest
{
    public string Code { get; set; } = string.Empty;
    public int InvoiceId { get; set; }
}