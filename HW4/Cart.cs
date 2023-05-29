using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4
{
    public class Cart
    {
        public int userId { get; }
        public Dictionary<Item, int> cart { get; }
        public Cart(int userId)
        {
            this.userId = userId;
            cart = new Dictionary<Item, int>();
        }
        public decimal CartAmount
        {
            get
            {
                decimal sum = 0;
                foreach (var pair in this.cart)
                {
                    sum += pair.Key.price * pair.Value;
                }
                return sum;
            }
        }
        public void AddItem(Item item)
        {
            if (item != null)
            {
                if (cart.ContainsKey(item))
                {
                    var quantity = cart[item];
                    cart[item] = quantity + 1;
                }
                else
                {
                    cart[item] = 1;
                }
            }
        }

        public bool RemoveItem(Item item)
        {
            if (item != null)
            {
                return cart.Remove(item);
            }
            else
            {
                return false;
            }
        }

       public int GetItemCount()
       {
           return cart.Count;
       }
    }
}
