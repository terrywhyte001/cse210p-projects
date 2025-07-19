using System;

class Program
{
    static void Main(string[] args)
    {
        Scripture scripture = LoadRandomScripture();

        while (!scripture.IsCompletelyHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to continue or type 'quit' to exit:");
            string input = Console.ReadLine().Trim().ToLower();

            if (input == "quit")
                break;

            scripture.HideRandomWords(3); // You can adjust this number if needed
        }

        Console.Clear();
        Console.WriteLine(scripture.GetDisplayText());
        Console.WriteLine("\nAll words are hidden. Program has ended.");
    }

    // Place LoadRandomScripture right below Main
    public static Scripture LoadRandomScripture()
    {
        var lines = File.ReadAllLines("scriptures.txt");
        Random rand = new Random();
        string line = lines[rand.Next(lines.Length)];
        string[] parts = line.Split('|');

        if (parts.Length == 4)
        {
            Reference reference = new Reference(parts[0], int.Parse(parts[1]), int.Parse(parts[2]));
            return new Scripture(reference, parts[3]);
        }
        else if (parts.Length == 5)
        {
            Reference reference = new Reference(parts[0], int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
            return new Scripture(reference, parts[4]);
        }

        throw new FormatException("Invalid scripture format.");
    }
}
