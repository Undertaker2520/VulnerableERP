using BusinessLayer.Interfaces;
using Common.DTOs;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore; 

namespace BusinessLayer.Services;

public class CouponService : ICouponService
{
    private readonly AppDbContext _context;

    public CouponService(AppDbContext context) => _context = context;

    public async Task<string> ValidateCouponAsync(ApplyCouponRequest request)
    {
        var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Code == request.Code);
        if (coupon == null) return "Coupon non valido.";
        if (coupon.IsUsed) return "Coupon già utilizzato.";
        return "OK";
    }

    public async Task<string> ApplyCouponAsync(ApplyCouponRequest request)
    {
        var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Code == request.Code);
        var invoice = await _context.Invoices.FindAsync(request.InvoiceId);

        if (coupon == null || invoice == null)
            return "Coupon o Fattura non validi.";

        // VULNERABILITÀ TOCTOU (Time-of-Check to Time-of-Use)
        // La validazione è avvenuta prima del popup. Qui non ricontrolliamo apposta lo stato IsUsed.
        
        invoice.Amount -= coupon.DiscountAmount;
        coupon.IsUsed = true;

        await _context.SaveChangesAsync();
        return $"Sconto di {coupon.DiscountAmount} applicato con successo!";
    }
}