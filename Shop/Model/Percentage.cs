using Shop.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    public struct Percentage : IEquatable<Percentage>
    {
        private decimal value;

        public decimal Value
        {
            get => value;
            set
            {
                if (value >= 0 && value <= 1)
                {
                    this.value = value;
                }
                else
                    throw new PercentageException($"Cannot set {value} as percentage value");
            }
        }

        public Percentage(decimal percents) : this()
        {
            Value = percents;
        }
        public Percentage(double percents) : this()
        {
            Value = (decimal)percents;
        }

        public override string ToString()
        {
            return $"{Value*100}%";
        }

        public override int GetHashCode()
        {
            var hashCode = 1927018180;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + value.GetHashCode();
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Percentage p1, Percentage p2)
        {
            if (null == p1)
                return (null == p2);

            return p1.Equals(p2);
        }

        public static bool operator !=(Percentage p1, Percentage p2)
        {
            if (null == p1)
                return (null != p2);

            return ! p1.Equals(p2);
        }

        public bool Equals(Percentage other)
        {
            return value == other.value &&
                  Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
