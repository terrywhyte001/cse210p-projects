using System;

namespace EternalQuest
{
    // One-time goal: complete once and receive points.
    public class SimpleGoal : Goal
    {
        private bool _isComplete;

        public SimpleGoal(string name, string description, int points) 
            : base(name, description, points)
        {
            _isComplete = false;
        }

        public override int RecordEvent()
        {
            if (!_isComplete)
            {
                _isComplete = true;
                return _points;
            }
            return 0;
        }

        public override bool IsComplete()
        {
            return _isComplete;
        }

        public override string ToSaveString()
        {
            // Format: Simple|name|description|points|isComplete
            return $"Simple|{Escape(_name)}|{Escape(_description)}|{_points}|{_isComplete}";
        }

        public override string GetStatusString()
        {
            return _isComplete ? "[X]" : "[ ]";
        }

        private string Escape(string s) => s.Replace("|", "¦"); // simple escape
    }
}

