using System.Drawing;

namespace VendingMachine
{
    class Product
    {
        private static int counter = 1;
        private decimal price = 0;
        public int UID { get; }
        public string Name { get; private set; }
        public int Amount { get; private set; }
        public decimal Price
        {
            get => price;
            set
            {
                if (value > 0) price = value;
            }

        }
        public Image Image { get; private set; }

        public Product(string name, int amount, decimal price, string image)
        {
            UID = counter;
            counter++;
            Name = name;
            Amount = amount;
            Price = price;
            Image = Image.FromFile(image);
        }

        public Product(string name, int amount, decimal price, Image image)
        {
            UID = counter;
            counter++;
            Name = name;
            Amount = amount;
            Price = price;
            Image = image;
        }

        //assuming 10 is the limit
        public bool AddPiece()
        {
            if (Amount < 10)
            {
                Amount++;
                return true;
            }
            return false;
        }

        public bool Sell()
        {
            if (Amount > 0)
            {
                Amount--;
                return true;
            }
            return false;
        }
    }
}
