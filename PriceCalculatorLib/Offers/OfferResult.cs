namespace PriceCalculatorLib.Offers
{
    public class OfferResult
    {
        public string Description { get; }
        public decimal Discount { get; }

        public OfferResult(string description, decimal discount)
        {
            Description = description;
            Discount = discount;
        }
    }
}
