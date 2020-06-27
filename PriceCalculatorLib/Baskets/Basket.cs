using System.Collections.Generic;
using System.Linq;
using PriceCalculatorLib.Products;

namespace PriceCalculatorLib.Baskets
{
    public class Basket
    {
        private readonly HashSet<LineItem> _lineItems = new HashSet<LineItem>();

        public void AddLineItem(LineItem lineItem)
        {
            var existingLineItem = _lineItems.FirstOrDefault(li => li == lineItem);
            if (existingLineItem != null)
            {
                existingLineItem.Quantity += lineItem.Quantity;
            }
            else
            {
                _lineItems.Add(lineItem);
            }
        }  

        public void AddLineItems(IEnumerable<LineItem> lineItem)
        {
            foreach (var product in lineItem)
            {
                AddLineItem(product);
            }
        }

        public int GetQuantity(Product product)
        {
            return _lineItems
                .Where(li => li.Product == product)
                .Sum(li => li.Quantity);
        }

        public IEnumerable<LineItem> GetLineItems()
        {
            return _lineItems;
        }

 
    }
}
