
using System;
using System.Collections.Generic;
using System.Diagnostics;
using HW4;

Cart cart = new Cart(1); // create a new cart object with user ID 1
bool shopping = true;
while (shopping)
{
    Console.WriteLine("Enter 'add' to add an item to the cart, 'remove' to remove an item, 'cart' to view the cart amount, or 'exit' to quit:");
    string userInput = Console.ReadLine().ToLower();
    try
    {
        switch (userInput)
        {
            case "add":
                handleAddAction(cart);
                break;
            case "remove":
                handleRemoveAction(cart);
                break;
            case "cart":
                handleShowCartAction(cart);
                break;
            case "exit":
                shopping = false;
                break;
            default:
                Console.WriteLine("Invalid input. Please try again.");
                break;
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (FormatException ex)
    {
        Console.WriteLine("Invalid format. Please enter a numeric value.", ex.Message);
    }

}

static void handleAddAction(Cart cart)
{
    Console.WriteLine("Enter the name of the item to add:");
    string itemName = Console.ReadLine();
    Console.WriteLine("Enter the price of the item:");
    decimal itemPrice = decimal.Parse(Console.ReadLine());
    if (itemPrice < 0)
    {
        throw new ArgumentException(String.Format("Price must be > 0"));
    }
    Console.WriteLine("Enter the quantity:");
    int itemQuantity = int.Parse(Console.ReadLine());
    Item item = new Item(itemName, itemPrice);
    if (itemQuantity <= 0)
    {
        throw new ArgumentException(String.Format("Quantity must be > 0"));
    }
    for (int i = 0; i < itemQuantity; i++)
    {
        cart.AddItem(item);
    }
    Console.WriteLine($"{item.name} added to cart.");
    if (itemQuantity <= 0)
    {
        throw new ArgumentException(String.Format("Quantity must be > 0"));
    }
}

static void handleRemoveAction(Cart cart)
{
    Console.WriteLine("Enter the name of the item to remove:");
    string itemToRemove = Console.ReadLine();
    bool itemRemoved = false;
    foreach (var pair in cart.cart)
    {
        if (pair.Key.name == itemToRemove)
        {
            cart.RemoveItem(pair.Key);
            Console.WriteLine($"{pair.Key.name} removed from cart.");
            itemRemoved = true;
            break;
        }
    }
    if (!itemRemoved)
    {
        Console.WriteLine("Item not found in cart.");
    }
}

static void handleShowCartAction(Cart cart)
{
    Console.WriteLine($"Cart amount: ${cart.CartAmount}");
}

/*
The code above contains two classes: Cart and Item.
 
The Cart class

- Encapsulates the shopping cart logic, including adding and removing items, and calculating the total cart amount. 
The Cart class contains private fields, such as userID and cart, which are not directly accessible from outside the class. 
Instead, the class provides public methods, such as AddItem() and RemoveItem(), to modify the cart's contents. 
This demonstrates encapsulation, which hides the implementation details of the class from other parts of the program.
- The abstraction principle is implemented in the Cart class by cart's total amount. 
which is the sum of the prices of all the items in the cart. 
This information is exposed to the main program through the CartAmount property.

The Item class

Encapsulation: The implementation details of the Item class are hidden from the main program.
The main program only interacts with the Item class by creating new Item objects
using the Item constructor and accessing the name and price fields of an Item object using the Item.Name and Item.Price properties. 
All the other implementation details such as how the name and price fields are stored and retrieved are encapsulated and hidden from the main program.

Abstraction: The Item class abstracts away the complexity of the item by only exposing its name and price to the main program. 
The main program does not need to know how the Item class stores and retrieves the name and price fields. 
It only needs to know the name and price of the item, which are easily accessed using the Item.Name and Item.Price properties.

*/













