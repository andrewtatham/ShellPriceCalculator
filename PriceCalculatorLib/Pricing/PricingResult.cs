using System.Collections.Generic;
using System.Linq;
using PriceCalculatorLib.Offers;

namespace PriceCalculatorLib.Pricing
{
    public class PricingResult
    {
        public decimal Subtotal { get; }
        public decimal Discount { get; }
        public decimal Total { get; }
        public IEnumerable<OfferResult> Offers { get; }

        public PricingResult(decimal subtotal, IEnumerable<OfferResult> offers)
        {
            Subtotal = subtotal;
            Offers = offers;
            Discount = (Offers?.Sum(o => o.Discount)).GetValueOrDefault();
            Total = Subtotal - Discount;
        }
    }
}