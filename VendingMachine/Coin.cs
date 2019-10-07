using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    abstract class Coin
    {
        public decimal Value { get; }

        protected Coin(decimal value)
        {
            Value = value;
        }
    }
}
