using System;

namespace PriceCalculatorLib.Helper
{
    public static class FormatHelper
    {
        public static string FormatPrice(decimal price)
        {
            if (price != decimal.Zero && decimal.MinusOne < price && price < decimal.One)
            {
                var pence = Convert.ToInt32(price * 100m);
                return $"{pence}p";
            }
            return $"{price:C2}";
        }
    }
}