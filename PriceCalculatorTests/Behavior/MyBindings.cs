using System;
using System.Collections.Generic;
using NUnit.Framework;
using PriceCalculatorLib.Baskets;
using TechTalk.SpecFlow;

namespace PriceCalculatorTests.Behavior
{
    [Binding]
    public class MyBindings
    {
        private readonly BasketPricingContext _context = new BasketPricingContext();
        private readonly Parser _parser;

        public MyBindings()
        {
            _parser = new Parser(_context);
        }

        [Given(@"the prices are")]
        public void GivenThePricesAre(Table table)
        {
            _context.PricingService.AddProductPrices(_parser.ParsePrices(table));
        }

        [Given(@"the discounts are")]
        public void GivenTheDiscountsAre(Table table)
        {
            _context.PricingService.AddOffers(_parser.ParseDiscountOffers(table));
        }

        [Given(@"the multibuy offers are")]
        public void GivenTheMultibuyOffersAre(Table table)
        {
            _context.PricingService.AddOffers(_parser.ParseMultibuyOffers(table));
        }

        [When(@"the basket contains")]
        public void WhenTheBasketContains(Table table)
        {
            try
            {
                _context.Exception = null;
                _context.Result = null;

                IEnumerable<LineItem> lineItems = _parser.ParseLineItems(table);
                _context.Basket = new Basket();
                _context.Basket.AddLineItems(lineItems);
                _context.Result = _context.PricingService.PriceBasket(_context.Basket);
            }
            catch (Exception e)
            {
                _context.Exception = e;
            }

        }

        [Then(@"the subtotal should be £(.*)")]
        public void ThenTheSubtotalShouldBe(decimal expectedSubtotal)
        {
            Assert.AreEqual(expectedSubtotal, _context.Result?.Subtotal);
        }

        [Then(@"the offers should save £(.*)")]
        public void ThenTheOffersShouldSave(decimal expectedDiscount)
        {
            Assert.AreEqual(expectedDiscount, _context.Result?.Discount);
        }

        [Then(@"the total should be £(.*)")]
        public void ThenTheTotalShouldBe(decimal expectedTotal)
        {
            Assert.AreEqual(expectedTotal, _context.Result?.Total);
        }

        [Then(@"the exception should be of type (.*)")]
        public void ThenTheExceptionShouldBeOfType(string type)
        {
            var expected = Type.GetType($"PriceCalculatorLib.Exceptions.{type}, PriceCalculatorLib");
            Assert.IsInstanceOf(expected, _context.Exception);
        }

        [Then(@"the exception message should be ""(.*)""")]
        public void ThenTheExceptionMessageShouldBe(string message)
        {
            Assert.AreEqual(message, _context.Exception?.Message);
        }


    }
}
