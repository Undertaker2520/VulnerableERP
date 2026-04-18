using BusinessLayer.Interfaces;
using Common.DTOs;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore; 

namespace BusinessLayer.Services;

public class CouponService : ICouponService
{
    private readonly AppDbContext _context;

    public CouponService(AppDbContext context) => _context = context;

    public async Task<string> ApplyCouponAsync(ApplyCouponRequest request)
    {
        // Recupero asincrono dei dati
        var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Code == request.Code);
        var invoice = await _context.Invoices.FindAsync(request.InvoiceId);

        if (coupon == null || invoice == null)
            return "Coupon o Fattura non validi.";

        // VULNERABILITÀ: RACE CONDITION
        // Senza un sistema di lock (es. lock pessimistico sul DB), 
        // più task leggeranno IsUsed = false contemporaneamente.
        if (coupon.IsUsed)
            return "Coupon già utilizzato.";

        // Task.Delay è perfetto qui: libera il thread permettendo a 
        // decine di altre richieste di entrare in questa sezione di codice.
        await Task.Delay(500);

        invoice.Amount -= coupon.DiscountAmount;
        coupon.IsUsed = true;

        await _context.SaveChangesAsync();
        return $"Sconto di {coupon.DiscountAmount} applicato con successo!";
    }
}