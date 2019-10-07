using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class InsufficientCoinInventory : State
    {
        public InsufficientCoinInventory()
        {
            throw new Exception("Insufficient coin inventory.");
        }
        public void BuyProduct(VendingMachine machine)
        {
            machine.State = new Awaiting();
        }
    }
}
