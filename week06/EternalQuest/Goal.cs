using System;

namespace EternalQuest
{
    // Base class for all goals
    public abstract class Goal
    {
        protected string _name;
        protected string _description;
        protected int _points;

        public string Name => _name;
        public string Description => _description;
        public int Points => _points;

        protected Goal(string name, string description, int points)
        {
            _name = name;
            _description = description;
            _points = points;
        }

        // Record an event for this goal. Returns points awarded (can be negative).
        public abstract int RecordEvent();

        // Whether the goal is completed (for eternal goals returns false).
        public abstract bool IsComplete();

        // A textual form used for saving/loading. Derived classes should extend this.
        public abstract string ToSaveString();

        // A user-facing short status (e.g., "[X]" or "[ ]", or progress).
        public abstract string GetStatusString();
    }
}
