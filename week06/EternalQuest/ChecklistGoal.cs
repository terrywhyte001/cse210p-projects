using System;

namespace EternalQuest
{
    // A goal that needs to be done N times, awards a bonus when finished.
    public class ChecklistGoal : Goal
    {
        private int _targetCount;
        private int _currentCount;
        private int _bonus;
        private bool _isComplete;

        public ChecklistGoal(string name, string description, int points, int targetCount, int bonus)
            : base(name, description, points)
        {
            _targetCount = targetCount;
            _bonus = bonus;
            _currentCount = 0;
            _isComplete = false;
        }

        public override int RecordEvent()
        {
            if (!_isComplete)
            {
                _currentCount++;
                if (_currentCount >= _targetCount)
                {
                    _isComplete = true;
                    return _points + _bonus;
                }
                return _points;
            }
            return 0;
        }

        public override bool IsComplete() => _isComplete;

        public override string ToSaveString()
        {
            return $"Checklist|{Escape(_name)}|{Escape(_description)}|{_points}|{_targetCount}|{_currentCount}|{_bonus}";
        }

        public override string GetStatusString()
        {
            return _isComplete ? "[X]" : $"[ ] ({_currentCount}/{_targetCount})";
        }

        private string Escape(string s) => s.Replace("|", "¦");
    }
}

