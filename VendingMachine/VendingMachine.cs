using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class VendingMachine
    {
        internal List<Coin> Coins { get; set; }
        public List<Product> Products { get; private set; }
        public State State { get; internal set; }
        internal List<Coin> DepositCoins { get; set; }
        public decimal Deposit { get; internal set; }
        public Product SelectedProduct { get; internal set; }

        public VendingMachine()
        {
            State = new Awaiting();
            Coins = new List<Coin>();
            DepositCoins = new List<Coin>();
            Products = new List<Product>();
        }

        public void BuyProduct()
        {
            State.BuyProduct(this);
        }

        public void SelectProduct(Product p) => SelectedProduct = p;

        public List<Coin> ReturnDeposit()
        {
            int[] change = CalculateCoinChange(Deposit);
            List<Coin> coinsToReturn = new List<Coin>(); //new list of coins to be returned
            coinsToReturn.AddRange(Enumerable.Repeat(new _2EurosCoin(), change[0])); //adding the count of 2 euro coins to the list
            coinsToReturn.AddRange(Enumerable.Repeat(new _1EuroCoin(), change[1]));
            coinsToReturn.AddRange(Enumerable.Repeat(new _50CentsCoin(), change[2]));
            coinsToReturn.AddRange(Enumerable.Repeat(new _20CentsCoin(), change[3]));
            coinsToReturn.AddRange(Enumerable.Repeat(new _10CentsCoin(), change[4]));
            coinsToReturn.AddRange(Enumerable.Repeat(new _5CentsCoin(), change[5]));

            //clearing deposit and returning the change to user
            DepositCoins.Clear();
            Deposit = 0;
            return coinsToReturn;
        }

        internal int[] CalculateCoinChange(decimal amount)
        {
            decimal toReturn = Math.Round(amount * 2, MidpointRounding.AwayFromZero) / 2; //rounding

            int _2s = (int)(toReturn / 2); //number of coins of 2 euros
            toReturn -= _2s * 2; //the rest of the change to be checked
            int _1s = (int)(toReturn / 1);
            toReturn -= _1s;
            int _50cents = (int)(toReturn / 0.5m);
            toReturn -= _50cents * 0.5m;
            int _20cents = (int)(toReturn / 0.2m);
            toReturn -= _20cents * 0.2m;
            int _10cents = (int)(toReturn / 0.1m);
            toReturn -= _10cents * 0.1m;
            int _5cents = (int)(toReturn / 0.05m);
            toReturn -= _5cents * 0.05m;

            return new int[6] { _2s, _1s, _50cents, _20cents, _10cents, _5cents };
        }

        /// <summary>
        /// Checks whether there are enough coins in the machine inventory for the change return
        /// </summary>
        /// <param name="count">Integer array with counts of euro coins of different sizes</param>
        /// <returns></returns>
        internal bool CountCoins(VendingMachine machine, int[] count)
        {
            if (machine.Coins.OfType<_2EurosCoin>().Count() < count[0]) return false;
            if (machine.Coins.OfType<_1EuroCoin>().Count() < count[1]) return false;
            if (machine.Coins.OfType<_50CentsCoin>().Count() < count[2]) return false;
            if (machine.Coins.OfType<_20CentsCoin>().Count() < count[3]) return false;
            if (machine.Coins.OfType<_10CentsCoin>().Count() < count[4]) return false;
            if (machine.Coins.OfType<_5CentsCoin>().Count() < count[5]) return false;
            return true;
        }


        public void Abort() => ReturnDeposit();

        public void AddDeposit(Coin c)
        {
            DepositCoins.Add(c);
            Deposit += c.Value;
        }

        //Max-capacity of coin storage not taken into consideration
        internal void AddCoins(List<Coin> coins)
        {
            coins.AddRange(coins);
        }

        internal void AddCountedCoins(int[] count)
        {
            Coins.AddRange(Enumerable.Repeat(new _2EurosCoin(), count[0]));
            Coins.AddRange(Enumerable.Repeat(new _1EuroCoin(), count[1]));
            Coins.AddRange(Enumerable.Repeat(new _50CentsCoin(), count[2]));
            Coins.AddRange(Enumerable.Repeat(new _20CentsCoin(), count[3]));
            Coins.AddRange(Enumerable.Repeat(new _10CentsCoin(), count[4]));
            Coins.AddRange(Enumerable.Repeat(new _5CentsCoin(), count[5]));
        }

        /// <summary>
        /// Removes number of coins of specified size
        /// </summary>
        /// <typeparam name="T">Size of coins</typeparam>
        /// <param name="count">How many coins to remove</param>
        /// <returns>Returns if operations is successful</returns>
        internal bool RemoveCoins<T>(int count)
        {
            List<Coin> toRemove = ((List<Coin>)Coins.OfType<T>()).ToList();
            if (toRemove.Count >= count)
            {
                for (int i = 0; i < count; i++)
                {
                    Coins.Remove(toRemove[i]);
                }
                return true;
            }
            return false;
        }

        internal void RemoveAllCoins() => Coins.Clear();

        internal bool AddProduct(Product p)
        {
            if (Products.Exists(x => x.Name == p.Name)) return false;
            else
            {
                Products.Add(p);
                return true;
            }
        }
        internal bool RemoveProduct(int id)
        {
            Product p = Products.FirstOrDefault(x => x.UID == id);
            if (p != null)
            {
                Products.Remove(p);
                return true;
            }
            return false;
        }
    }
}
