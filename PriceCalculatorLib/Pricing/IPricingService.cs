using System.Collections.Generic;
using PriceCalculatorLib.Baskets;
using PriceCalculatorLib.Offers;
using PriceCalculatorLib.Products;

namespace PriceCalculatorLib.Pricing
{
    public interface IPricingService
    {
        void AddProductPrice(ProductPrice productPrice);
        void AddProductPrices(IEnumerable<ProductPrice> productPrices);
        
        void AddOffer(IOffer offer);
        void AddOffers(IEnumerable<IOffer> offers);

        decimal GetUnitPrice(Product product);
        PricingResult PriceBasket(Basket basket);
    }
    
}