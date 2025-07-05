using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("what is your grade? ");
        string input = Console.ReadLine();
        int gradepercentage = int.Parse(input);

        string letter = "";
        string sign = "";

        // Determine the letter grade
        if (gradepercentage >= 90)
        {
            letter = "A";
        }
        else if (gradepercentage >= 80)
        {
            letter = "B";
        }
        else if (gradepercentage >= 70)
        {
            letter = "C";
        }
        else if (gradepercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }
        // Determine + or - sign
        int lastDigit = gradepercentage % 10;
        if (letter != "A" && letter != "F")
        {
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
            // No A+ in this grading system, No sign for A+
        }
        // F has no + or - so sign remains empty

        //display the letter grade
        Console.WriteLine($"Your letter grade is {letter}{sign}");

        //Determine the pass or fail status
        if (gradepercentage >= 70)
        {
            Console.WriteLine("congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("keep working hard! You can do it.");
        }

    


    }
}