using System.Linq;

namespace VendingMachine
{
    class ProductSelected : State
    {
        public void BuyProduct(VendingMachine machine)
        {
            if (machine.SelectedProduct.Price <= machine.Deposit)
            {
                machine.Coins.AddRange(machine.DepositCoins);
                machine.DepositCoins.Clear();
                Product p = machine.Products.FirstOrDefault(x => x.Name == machine.SelectedProduct.Name);
                int[] change = machine.CalculateCoinChange(machine.Deposit - p.Price); //how much change after buying
                if (machine.CountCoins(machine, change))
                {
                    if (p.Sell())
                    {
                        machine.Deposit -= p.Price;
                        machine.SelectedProduct = null;
                        machine.State = new Awaiting();
                    }
                    else machine.State = new InsufficientProductInventory();
                }
                else machine.State = new InsufficientCoinInventory();
            }
            else machine.State = new InsufficientDeposit();
        }
    }
}
