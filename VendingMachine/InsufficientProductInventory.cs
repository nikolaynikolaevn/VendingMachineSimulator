using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class InsufficientProductInventory : State
    {
        public InsufficientProductInventory()
        {
            throw new Exception("Insufficient product inventory.");
        }
        public void BuyProduct(VendingMachine machine)
        {
            machine.State = new Awaiting();
        }
    }
}
