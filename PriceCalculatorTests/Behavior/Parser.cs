using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PriceCalculatorLib.Baskets;
using PriceCalculatorLib.Offers;
using PriceCalculatorLib.Pricing;
using PriceCalculatorLib.Products;
using TechTalk.SpecFlow;

namespace PriceCalculatorTests.Behavior
{
    public class Parser
    {
        private readonly BasketPricingContext _context;

        public Parser(BasketPricingContext context)
        {
            _context = context;
        }

        private decimal ParsePercentage(string percentage)
        {
            percentage = percentage.Replace("%", string.Empty);
            return decimal.Parse(percentage, NumberStyles.Any) / 100m;
        }

        public IEnumerable<ProductPrice> ParsePrices(Table table)
        {
            return table.Rows.Select(ParseProductPrice);
        }

        private ProductPrice ParseProductPrice(TableRow row)
        {
            return new ProductPrice(
                new Product(row["Product"]),
                ParsePrice(row["Price"]));
        }

        private decimal ParsePrice(string price)
        {
            return decimal.Parse(price, NumberStyles.Currency);
        }

        public IEnumerable<LineItem> ParseLineItems(Table table)
        {
            return table.Rows.Select(ParseLineItem);
        }

        private LineItem ParseLineItem(TableRow row)
        {
            var quantityHeader = "Quantity";
            var quantity = row.ContainsKey(quantityHeader) ? int.Parse(row[quantityHeader]) : 1;
            return new LineItem(
                new Product(row["Product"]),
                quantity);
        }

        public IEnumerable<IOffer> ParseDiscountOffers(Table table)
        {
            return table.Rows.Select(ParseDiscountOffer);
        }

        private IOffer ParseDiscountOffer(TableRow row)
        {
            return new DiscountOffer(
                _context.PricingService,
                new Product(row["Product"]),
                ParsePercentage(row["Discount"]));
        }

        public IEnumerable<IOffer> ParseMultibuyOffers(Table table)
        {
            return table.Rows.Select(ParseMultibuyOffer);
        }

        private IOffer ParseMultibuyOffer(TableRow row)
        {
            return new MultibuyOffer(
                _context.PricingService,
                int.Parse(row["If n"]),
                new Product(row["If product"]),
                int.Parse(row["Then n"]),
                new Product(row["Then product"]),
                ParsePercentage(row["Discount"]));
        }
    }
}
