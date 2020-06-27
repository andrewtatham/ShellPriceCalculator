using System.Diagnostics;
using PriceCalculatorLib.Products;

namespace PriceCalculatorLib.Pricing
{
    [DebuggerDisplay("{" + nameof(Product) + "} {Price:C2}")]
    public class ProductPrice
    {
        public Product Product { get; }
        public decimal Price { get; }

        public ProductPrice(Product product, decimal price)
        {
            Product = product;
            Price = price;
        }
    }
}