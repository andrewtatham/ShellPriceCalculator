using PriceCalculatorLib.Baskets;
using PriceCalculatorLib.Helper;
using PriceCalculatorLib.Pricing;
using PriceCalculatorLib.Products;

namespace PriceCalculatorLib.Offers
{
    public class DiscountOffer : BaseOffer, IOffer
    {
        private readonly Product _product;
        private readonly decimal _discount;

        public DiscountOffer(IPricingService pricingService, Product product, decimal discount) : base(pricingService)
        {
            _product = product;
            _discount = discount;
        }
        public OfferResult ApplyOffer(Basket basket)
        {
            var quantity = basket.GetQuantity(_product);
            if (quantity <= 0) return null;
            var unitPrice = PricingService.GetUnitPrice(_product);
            var description = $"{_product.Name} {_discount:P0} off";
            var discount = RoundingHelper.RoundPrice(unitPrice * quantity * _discount);
            return new OfferResult(description, discount);
        }
    }
}