using System;

class Program
{
    static void Main(string[] args)
   {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Start Breathing Activity");
            Console.WriteLine("2. Start Reflection Activity");
            Console.WriteLine("3. Start Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Choose an option: ");

            string input = Console.ReadLine();

            if (input == "1")
            {
                BreathingActivity breathe = new BreathingActivity();
                breathe.Run();
            }
            else if (input == "2")
            {
                ReflectionActivity reflect = new ReflectionActivity();
                reflect.Run();
            }
            else if (input == "3")
            {
                ListingActivity list = new ListingActivity();
                list.Run();
            }
            else if (input == "4")
            {
                Console.WriteLine("Goodbye!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Press Enter to continue.");
                Console.ReadLine();
            }
        }
    }
}