using System;

class Program
{
    static void Main(string[] args)
    {
        Address addr1 = new Address("123 Elm St", "Springfield", "IL", "USA");
        Customer cust1 = new Customer("Alice Smith", addr1);
        Order order1 = new Order(cust1);
        order1.AddProduct(new Product("Book", "B001", 12.99, 2));
        order1.AddProduct(new Product("Pen", "P101", 1.49, 5));

        Address addr2 = new Address("456 Maple Rd", "Toronto", "ON", "Canada");
        Customer cust2 = new Customer("Bob Lee", addr2);
        Order order2 = new Order(cust2);
        order2.AddProduct(new Product("Notebook", "N300", 6.99, 3));
        order2.AddProduct(new Product("Stapler", "S999", 9.99, 1));

        DisplayOrder(order1);
        Console.WriteLine();
        DisplayOrder(order2);
    }

    static void DisplayOrder(Order order)
    {
        Console.WriteLine(order.GetPackingLabel());
        Console.WriteLine(order.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order.GetTotalPrice():F2}");
    }
}