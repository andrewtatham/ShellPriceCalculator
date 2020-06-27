using PriceCalculatorLib.Baskets;

namespace PriceCalculatorLib.Offers
{
    public interface IOffer
    {
        OfferResult ApplyOffer(Basket basket);
    }
}