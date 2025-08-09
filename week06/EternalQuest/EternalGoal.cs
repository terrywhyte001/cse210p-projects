using System;

namespace EternalQuest
{
    // A goal you can complete repeatedly; never marked complete
    public class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points)
        {
        }

        public override int RecordEvent()
        {
            return _points;
        }

        public override bool IsComplete()
        {
            return false;
        }

        public override string ToSaveString()
        {
            // Format: Eternal|name|description|points
            return $"Eternal|{Escape(_name)}|{Escape(_description)}|{_points}";
        }

        public override string GetStatusString()
        {
            return "[~]"; // indicates eternal
        }

        private string Escape(string s) => s.Replace("|", "¦");
    }
}
