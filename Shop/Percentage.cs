using Shop.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    public struct Percentage
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
    }
}
