using System;
using System.Diagnostics;

namespace PriceCalculatorLib.Products
{
    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class Product : IEquatable<Product>
    {
        public string Name { get; }
  
        public Product(string name)
        {
            Name = name;
        }

        public bool Equals(Product other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Product) obj);
        }

        public override int GetHashCode()
        {
            return Name.ToLowerInvariant().GetHashCode();
        }

        public static bool operator ==(Product left, Product right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Product left, Product right)
        {
            return !Equals(left, right);
        }
    }
}
