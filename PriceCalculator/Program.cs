using System;
using System.Linq;
using PriceCalculatorLib.Baskets;
using PriceCalculatorLib.Exceptions;
using PriceCalculatorLib.Helper;
using PriceCalculatorLib.Offers;
using PriceCalculatorLib.Pricing;
using PriceCalculatorLib.Products;

namespace PriceCalculator
{
    public static class Program
    {
        public static IPricingService PricingService { get; set; } = new PricingService();
        public static bool WaitForKeyPress { get; set; } = true;
        public static event EventHandler<string> LogMessageEvent;

        static Program()
        {
            LogMessageEvent += (sender, message) => Console.WriteLine(message);
        }

        public static int Main(string[] args)
        {
            // TODO Move to config
            ExampleConfiguration();

            var basket = new Basket();
            foreach (var arg in args)
            {
                basket.AddLineItem(new LineItem(new Product(arg), 1));
            }

            try
            {
                var result = PricingService.PriceBasket(basket);
                LogResult(result);

                return 0;
            }
            catch (UnrecognisedProductsException e)
            {
                Log($"Could not find prices for: {e.Message}");
                return 2;
            }
            catch (Exception e)
            {
                Log($"An exception occurred: {e.Message}");
                return 1;
            }
            finally
            {
                if (WaitForKeyPress)
                {
                    Log("Please press any key to exit");
                    Console.ReadKey();
                }

            }

        }

        private static void ExampleConfiguration()
        {
            PricingService.AddProductPrices(new[]
            {
                new ProductPrice(new Product("Beans"), 0.65m),
                new ProductPrice(new Product("Bread"), 0.80m),
                new ProductPrice(new Product("Milk"), 1.30m),
                new ProductPrice(new Product("Apples"), 1m)
            });

            PricingService.AddOffers(new IOffer[]
            {
                new DiscountOffer(PricingService,
                    new Product("Apples"),
                    0.1m),
                new MultibuyOffer(PricingService,
                    2,
                    new Product("Apples"),
                    1,
                    new Product("Bread"),
                    0.5m)
            });
        }

        private static void LogResult(PricingResult result)
        {
            if (result == null)
            {
                Log("Pricing service returned null response");
            }
            else
            {
                Log($"Subtotal: {result.Subtotal:C2}");
                if (result.Offers != null && result.Offers.Any())
                {
                    foreach (var offer in result.Offers)
                    {
                        Log($"{offer.Description}: {FormatHelper.FormatPrice(-offer.Discount)}");
                    }
                }
                else
                {
                    Log("(No offers available)");
                }

                Log($"Total price: {result.Total:C2}");
            }
        }

        private static void Log(string message)
        {
            LogMessageEvent?.Invoke(null, message);
        }
    }
}
