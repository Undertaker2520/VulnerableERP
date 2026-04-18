using BusinessLayer.Interfaces;
using Common.DTOs;
using DataAccessLayer;

namespace BusinessLayer.Services
{
    public class CouponService : ICouponService
    {
        private readonly AppDbContext _context;

        public CouponService(AppDbContext context) => _context = context;

        public string ApplyCoupon(ApplyCouponRequest request)
        {
            var coupon = _context.Coupons.FirstOrDefault(c => c.Code == request.Code);
            var invoice = _context.Invoices.Find(request.InvoiceId);

            if (coupon == null || invoice == null)
                return "Coupon o Fattura non validi.";

            //RACE CONDITION
            //Se 2 richieste arrivano contemporaneamente, entrambe leggeranno IsUsed = false
            if (coupon.IsUsed)
                return "Coupon già utilizzato.";

            //allarghiamo artificialmente la finestra di ritardo per facilitare l'attacco
            Thread.Sleep(500);

            invoice.Amount -= coupon.DiscountAmount;
            coupon.IsUsed = true; //solo ora viene segnato come usato

            _context.SaveChanges();
            return $"Sconto di {coupon.DiscountAmount} applicato con successo!";
        }
    }
}