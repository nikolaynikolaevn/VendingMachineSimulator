using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Awaiting : State
    {
        public void BuyProduct(VendingMachine machine)
        {
            if (machine.SelectedProduct == null) throw new Exception("Please select a product.");
            else
            {
                machine.State = new ProductSelected();
                machine.BuyProduct();
            }
        }
    }
}
