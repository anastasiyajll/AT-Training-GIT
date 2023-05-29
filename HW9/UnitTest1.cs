using NUnit.Framework;
using HW4;
namespace TestCart1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("SetUp done");
        }
        [Test]
        public void AddingItems()
        {
            Cart cart = new Cart(1);
            Item item = new Item("Item 1", 10.0m);
            cart.AddItem(item);
            Assert.That(cart.GetItemCount(), Is.EqualTo(1));
        }
        [Test]
        public void RemovingItems()
        {
            Cart cart = new Cart(1);
            Item item = new Item("Item 1", 10.0m);
            cart.AddItem(item);
            cart.RemoveItem(item);
            Assert.AreEqual(0, cart.GetItemCount());
        }
        [Test]
        public void CalculatingTotalPrice()
        {
            Cart cart = new Cart(1);
            Item item1 = new Item("Item 1", 10.0m);
            Item item2 = new Item("Item 2", 5.0m);
            cart.AddItem(item1);
            cart.AddItem(item2);

            decimal cartAmount = cart.CartAmount;

            Assert.AreEqual(15.0m, cartAmount);
        }
    }
}