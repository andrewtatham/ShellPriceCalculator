using System.Collections.Generic;
using System.Linq;
using PriceCalculatorLib.Baskets;
using PriceCalculatorLib.Exceptions;
using PriceCalculatorLib.Offers;
using PriceCalculatorLib.Products;

namespace PriceCalculatorLib.Pricing
{
    public class PricingService : IPricingService
    {

        private readonly HashSet<ProductPrice> _productPrices = new HashSet<ProductPrice>();
        private readonly List<IOffer> _offers = new List<IOffer>();

        public void AddProductPrice(ProductPrice productPrice)
        {
            _productPrices.Add(productPrice);
        }

        public void AddProductPrices(IEnumerable<ProductPrice> productPrices)
        {
            foreach (var productPrice in productPrices)
            {
                AddProductPrice(productPrice);
            }
        }

        public void AddOffer(IOffer offer)
        {
            _offers.Add(offer);
        }

        public void AddOffers(IEnumerable<IOffer> offers)
        {
            foreach (var offer in offers)
            {
                AddOffer(offer);
            }
        }

        public PricingResult PriceBasket(Basket basket)
        {
            var subtotal = GetBasketSubtotal(basket);
            var offers = _offers
                .Select(offer => offer.ApplyOffer(basket))
                .Where(offer => offer != null)
                .ToList();
           
            return new PricingResult(subtotal, offers);
        }

        private decimal GetBasketSubtotal(Basket basket)
        {
            var lineItems = basket.GetLineItems();
            List<Product> productsNotFound = new List<Product>();

            var lineItemPrices = lineItems.Select(li =>
            {
                var productPrice = GetProductPrice(li.Product);
                if (productPrice == null)
                    productsNotFound.Add(li.Product);
                return (productPrice?.Price).GetValueOrDefault() * li.Quantity;
            }).ToList();

            if (productsNotFound.Any())
            {
                throw new UnrecognisedProductsException($"{string.Join(", ", productsNotFound.Select(p => p.Name))}");
            }

            var subtotal = lineItemPrices.Sum();
            return subtotal;
        }

        private ProductPrice GetProductPrice(Product product)
        {
            return _productPrices.FirstOrDefault(pp => pp.Product == product);
        }

        public decimal GetUnitPrice(Product product)
        {
            var productPrice = GetProductPrice(product);
            return productPrice.Price;
        }
    }
}