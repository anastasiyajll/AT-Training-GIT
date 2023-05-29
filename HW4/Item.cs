using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4
{
    public class Item
    {
        private readonly int itemQuantity;

        public string name { get; set; }
        public int price { get; }
        public Item(string name, decimal price)
        {
            /*
            if (price < 0)
            {
                throw new ArgumentException(String.Format("Price must be > 0", price));
            }
            if (price < 0)
            {
                throw new ArgumentException("Price should be positive");
            }
            */
            this.name = name;
            this.price = (int)price;
            /*
            if (itemQuantity <= 0)
            {
                throw new ArgumentException(String.Format("Quantity must be > 0"));
            }
            */
        } 

    }
}
