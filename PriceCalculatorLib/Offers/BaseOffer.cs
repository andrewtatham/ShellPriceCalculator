using PriceCalculatorLib.Pricing;

namespace PriceCalculatorLib.Offers
{
    public abstract class BaseOffer
    {
        private protected readonly IPricingService PricingService;

        protected BaseOffer(IPricingService pricingService)
        {
            PricingService = pricingService;
        }
    }
}
