using System;

public class BreathingActivity : Activity
{
    public BreathingActivity() 
        : base("Breathing Activity", 
        "This activity will help you relax by guiding you through slow breathing.") {}

    public void Run()
    {
        DisplayStartingMessage();
        int timePassed = 0;

        while (timePassed < GetDuration())
        {
            Console.Write("\nBreathe in... ");
            ShowCountdown(4);
            timePassed += 4;

            if (timePassed >= GetDuration()) break;

            Console.Write("\nBreathe out... ");
            ShowCountdown(4);
            timePassed += 4;
        }

        DisplayEndingMessage();
    }
}
