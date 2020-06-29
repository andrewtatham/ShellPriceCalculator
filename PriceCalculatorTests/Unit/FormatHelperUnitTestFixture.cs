using NUnit.Framework;
using PriceCalculatorLib.Helper;

namespace PriceCalculatorTests.Unit
{
    [TestFixture]
    [Culture("en-GB")]
    public class FormatHelperUnitTestFixture
    {
        [Test]
        [TestCase(1, "£1.00")]
        [TestCase(0.99d, "99p")]
        [TestCase(0.01d, "1p")]
        [TestCase(0, "£0.00")]
        [TestCase(-0.01d, "-1p")]
        [TestCase(-0.99d, "-99p")]
        [TestCase(-1, "-£1.00")]
        public void Format(decimal input, string expected)
        {
            var actual = FormatHelper.FormatPrice(input);
            Assert.AreEqual(expected, actual);
        }
    }
}
