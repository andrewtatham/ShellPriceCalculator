using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using PriceCalculator;
using PriceCalculatorLib.Baskets;
using PriceCalculatorLib.Exceptions;
using PriceCalculatorLib.Offers;
using PriceCalculatorLib.Pricing;

namespace PriceCalculatorTests.Unit
{
    [TestFixture]
    public static class ProgramUnitTestFixture
    {
        private static readonly Mock<IPricingService> MockPricingService = new Mock<IPricingService>();
        private static readonly List<string> LogMessages = new List<string>();

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            Program.PricingService = MockPricingService.Object;
            Program.WaitForKeyPress = false;
            Program.LogMessageEvent += OnLogMessageEvent;
        }

        private static void OnLogMessageEvent(object sender, string message)
        {
            LogMessages.Add(message);
        }

        private static bool IsLogged(string message)
        {
            return LogMessages.Contains(message);
        }

        private static void Act()
        {
            Program.Main(new[] { "Apples", "Milk", "Bread" });
        }

        [SetUp]
        public static void SetUp()
        {
            MockPricingService.Invocations.Clear();
            LogMessages.Clear();
        }

        [Test]
        public static void VerifyPricesAreSet()
        {
            Act();

            MockPricingService.Verify(x => x.AddProductPrices(It.IsAny<IEnumerable<ProductPrice>>()));
        }

        [Test]
        public static void VerifyOffersAreSet()
        {
            Act();

            MockPricingService.Verify(x => x.AddOffers(It.IsAny<IEnumerable<IOffer>>()));
        }

        [Test]
        public static void VerifyPricingServiceCalled()
        {
            Act();

            MockPricingService.Verify(x => x.PriceBasket(It.IsAny<Basket>()));
        }

        [Test]
        public static void VerifyPricingServiceReturnsNull()
        {
            MockPricingService
                .Setup(x => x.PriceBasket(It.IsAny<Basket>()))
                .Returns((Basket basket) => default);

            Act();

            Assert.IsTrue(IsLogged("Pricing service returned null response"));
        }

        [Test]
        public static void VerifyPricingServiceReturnsResultWithOffers()
        {
            MockPricingService
                .Setup(x => x.PriceBasket(It.IsAny<Basket>()))
                .Returns((Basket basket) => new PricingResult(5.99m, new[]
                {
                    new OfferResult("Offer 1", 0.55m),
                    new OfferResult("Offer 2", 1.45m)
                }));

            Act();

            Assert.IsTrue(IsLogged("Subtotal: £5.99"));
            Assert.IsTrue(IsLogged("Offer 1: -55p"));
            Assert.IsTrue(IsLogged("Offer 2: -£1.45"));
            Assert.IsTrue(IsLogged("Total price: £3.99"));
        }

        [Test]
        public static void VerifyPricingServiceReturnsResultWithNullOffers()
        {
            MockPricingService
                .Setup(x => x.PriceBasket(It.IsAny<Basket>()))
                .Returns((Basket basket) => new PricingResult(5.99m, null));

            Act();

            Assert.IsTrue(IsLogged("Subtotal: £5.99"));
            Assert.IsTrue(IsLogged("(No offers available)"));
            Assert.IsTrue(IsLogged("Total price: £5.99"));
        }

        [Test]
        public static void VerifyPricingServiceReturnsResultWithEmptyOffers()
        {
            MockPricingService
                .Setup(x => x.PriceBasket(It.IsAny<Basket>()))
                .Returns((Basket basket) => new PricingResult(5.99m, new OfferResult[0]));

            Act();

            Assert.IsTrue(IsLogged("Subtotal: £5.99"));
            Assert.IsTrue(IsLogged("(No offers available)"));
            Assert.IsTrue(IsLogged("Total price: £5.99"));
        }

        [Test]
        public static void VerifyPricingServiceThrowsUnrecognisedProductsException()
        {
            MockPricingService
                .Setup(x => x.PriceBasket(It.IsAny<Basket>()))
                .Throws(new UnrecognisedProductsException("Eggs, Spam"));

            Act();

            Assert.IsTrue(IsLogged("Could not find prices for: Eggs, Spam"));
        }


        [Test]
        public static void VerifyPricingServiceThrowsException()
        {
            MockPricingService
                .Setup(x => x.PriceBasket(It.IsAny<Basket>()))
                .Throws(new Exception("<message>"));

            Act();

            Assert.IsTrue(IsLogged("An exception occurred: <message>"));
        }
    }
}