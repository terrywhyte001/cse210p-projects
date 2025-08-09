using System;

namespace EternalQuest
{
    // Creative extra: negative goal - recording it subtracts points (bad habit).
    public class NegativeGoal : Goal
    {
        public NegativeGoal(string name, string description, int penaltyPoints)
            : base(name, description, penaltyPoints)
        {
        }

        // Recording a negative habit removes points
        public override int RecordEvent()
        {
            // Returns negative points to be subtracted from score
            Console.WriteLine($"Recorded negative habit. You lose {_points} points.");
            return -_points;
        }

        public override bool IsComplete()
        {
            return false;
        }

        public override string ToSaveString()
        {
            // Format: Negative|name|description|penaltyPoints
            return $"Negative|{Escape(_name)}|{Escape(_description)}|{_points}";
        }

        public override string GetStatusString()
        {
            return "[!]";
        }

        private string Escape(string s) => s.Replace("|", "¦");
    }
}




