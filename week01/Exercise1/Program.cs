using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("what is your  first name? ");
        string first = Console.ReadLine();
       
       Console.Write("what is your last name? ");
        string last = Console.ReadLine();
        
        Console.Write($"your name is {last}, {first} {last}");
       
    
    }
}