using System;

namespace PriceCalculatorLib.Exceptions
{
    public class UnrecognisedProductsException : Exception
    {
        public UnrecognisedProductsException(string message) : base(message)
        {

        }
    }
}
