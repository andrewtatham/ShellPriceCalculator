using System;
using PriceCalculatorLib.Baskets;
using PriceCalculatorLib.Pricing;

namespace PriceCalculatorTests.Behavior
{
    public class BasketPricingContext
    {
        public PricingService PricingService { get; } = new PricingService();
        public Basket Basket { get; set; }
        public PricingResult Result { get; set; }
        public Exception Exception { get; set; }
    }
}
