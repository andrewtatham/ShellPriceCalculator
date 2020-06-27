using System;
using System.Diagnostics;
using PriceCalculatorLib.Products;

namespace PriceCalculatorLib.Baskets
{
    [DebuggerDisplay("{Quantity} x {Product}")]
    public class LineItem : IEquatable<LineItem>
    {
        public Product Product { get; }
        public int Quantity { get; set; }

        public LineItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public bool Equals(LineItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Product, other.Product);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LineItem) obj);
        }

        public override int GetHashCode()
        {
            return (Product != null ? Product.GetHashCode() : 0);
        }

        public static bool operator ==(LineItem left, LineItem right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LineItem left, LineItem right)
        {
            return !Equals(left, right);
        }
    }
}