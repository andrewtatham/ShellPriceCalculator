using System;
using PriceCalculatorLib.Baskets;
using PriceCalculatorLib.Helper;
using PriceCalculatorLib.Pricing;
using PriceCalculatorLib.Products;

namespace PriceCalculatorLib.Offers
{
    public class MultibuyOffer : BaseOffer, IOffer
    {
        private readonly int _ifQuantity;
        private readonly Product _ifProduct;
        private readonly int _thenQuantity;
        private readonly Product _thenProduct;
        private readonly decimal _discount;

        public MultibuyOffer(IPricingService pricingService, int ifQuantity, Product ifProduct, int thenQuantity, Product thenProduct, decimal discount) : base(pricingService)
        {
            _ifQuantity = ifQuantity;
            _ifProduct = ifProduct;
            _thenQuantity = thenQuantity;
            _thenProduct = thenProduct;
            _discount = discount;
        }

        public OfferResult ApplyOffer(Basket basket)
        {
            var ifProductQuantity = basket.GetQuantity(_ifProduct);
            if (ifProductQuantity > 0 && ifProductQuantity >= _ifQuantity)
            {
                var offerQuantity = ifProductQuantity / _ifQuantity;
                var thenProductQuantity = basket.GetQuantity(_thenProduct);
                var qualifyingProductQuantity = Math.Min(_thenQuantity * offerQuantity, thenProductQuantity);
                var unitPrice = PricingService.GetUnitPrice(_thenProduct);
                var discount = RoundingHelper.RoundPrice(qualifyingProductQuantity * unitPrice * _discount);
                var description = $"Buy {_ifQuantity} {_ifProduct.Name} get {_thenQuantity} {_thenProduct.Name} {_discount:P0} off";
                return new OfferResult(description, discount);
            }
            return null;
        }
    }
}