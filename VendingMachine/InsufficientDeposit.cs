using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class InsufficientDeposit : State
    {
        public InsufficientDeposit()
        {
            throw new Exception("Insufficient deposit.");
        }
        public void BuyProduct(VendingMachine machine)
        {
            machine.State = new Awaiting();
        }
    }
}
