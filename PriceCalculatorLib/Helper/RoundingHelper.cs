using System;

namespace PriceCalculatorLib.Helper
{
    public static class RoundingHelper
    {
        public static decimal RoundPrice(decimal price)
        {
            return Math.Round(price, 2, MidpointRounding.AwayFromZero);
        }
    }
}
