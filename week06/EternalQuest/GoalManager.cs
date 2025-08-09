using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    public class GoalManager
    {
        private List<Goal> _goals;
        private int _score;
        private int _level; // creative extra: leveling
        private int _pointsToLevel; // points required to reach next level

        public GoalManager()
        {
            _goals = new List<Goal>();
            _score = 0;
            _level = 1;
            _pointsToLevel = 1000; // example: every 1000 points = level up
        }

        public void AddGoal(Goal goal)
        {
            _goals.Add(goal);
            Console.WriteLine($"Added goal: {goal.Name}");
        }

        public void ListGoals()
        {
            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals yet.");
                return;
            }

            for (int i = 0; i < _goals.Count; i++)
            {
                Goal g = _goals[i];
                string status = g.GetStatusString();
                Console.WriteLine($"{i + 1}. {status} {g.Name} - {g.Description} (Points: {g.Points})");
                // For checklist goals the status string contains progress.
            }
        }

        public void DisplayScore()
        {
            Console.WriteLine($"Score: {_score} | Level: {_level} (Next level at {LevelThreshold(_level + 1)} pts)");
        }

        public void RecordEventByIndex(int index)
        {
            if (index < 0 || index >= _goals.Count)
            {
                Console.WriteLine("Invalid goal selection.");
                return;
            }

            Goal g = _goals[index];
            int awarded = g.RecordEvent();
            if (awarded != 0)
            {
                AddScore(awarded);
                Console.WriteLine($"Points awarded: {awarded}");
            }
            else
            {
                Console.WriteLine("No points awarded.");
            }
        }

        private void AddScore(int points)
        {
            _score += points;
            if (_score < 0) _score = 0; // do not allow negative total score
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            // Simple leveling: level increases every _pointsToLevel points.
            int newLevel = (_score / _pointsToLevel) + 1;
            if (newLevel > _level)
            {
                Console.WriteLine($"Congratulations! You've reached level {newLevel}!");
                _level = newLevel;
            }
        }

        private int LevelThreshold(int level)
        {
            return (level - 1) * _pointsToLevel;
        }

        public void SaveToFile(string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.WriteLine($"SCORE|{_score}|{_level}");
                foreach (Goal g in _goals)
                {
                    sw.WriteLine(g.ToSaveString());
                }
            }

            Console.WriteLine($"Saved {_goals.Count} goals and score {_score} to {filename}");
        }

        public void LoadFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("Save file not found.");
                return;
            }

            List<Goal> loaded = new List<Goal>();
            int loadedScore = 0;
            int loadedLevel = 1;

            string[] lines = File.ReadAllLines(filename);
            foreach (string rawLine in lines)
            {
                if (string.IsNullOrWhiteSpace(rawLine)) continue;
                string line = rawLine.Trim();

                string[] parts = line.Split('|');
                if (parts.Length == 0) continue;

                string type = parts[0];

                try
                {
                    switch (type)
                    {
                        case "SCORE":
                            if (parts.Length >= 3)
                            {
                                int.TryParse(parts[1], out loadedScore);
                                int.TryParse(parts[2], out loadedLevel);
                            }
                            break;

                        case "Simple":
                            // Simple|name|description|points|isComplete
                            if (parts.Length >= 5)
                            {
                                string name = Unescape(parts[1]);
                                string desc = Unescape(parts[2]);
                                int points = int.Parse(parts[3]);
                                bool isComplete = bool.Parse(parts[4]);
                                var simple = new SimpleGoal(name, desc, points);
                                if (isComplete)
                                {
                                    // mark complete by recording once without adding to score
                                    // internal field _isComplete is private; record by calling RecordEvent and then deduct points
                                    // Simpler: reconstruct by reflection? Instead, we can simulate by storing
                                    // alternative: create a new subclass constructor? To keep encapsulation simple, we'll simulate:
                                    if (isComplete)
                                    {
                                        // To mark as complete without awarding points:
                                        // call RecordEvent and then subtract the points we would otherwise add from loadedScore later.
                                        int awarded = simple.RecordEvent();
                                        loadedScore -= awarded;
                                    }
                                }
                                loaded.Add(simple);
                            }
                            break;

                        case "Eternal":
                            // Eternal|name|description|points
                            if (parts.Length >= 4)
                            {
                                string name = Unescape(parts[1]);
                                string desc = Unescape(parts[2]);
                                int points = int.Parse(parts[3]);
                                loaded.Add(new EternalGoal(name, desc, points));
                            }
                            break;

                        case "Checklist":
                            // Checklist|name|description|pointsPerEvent|targetCount|currentCount|bonusPoints|isComplete
                            if (parts.Length >= 8)
                            {
                                string name = Unescape(parts[1]);
                                string desc = Unescape(parts[2]);
                                int perEvent = int.Parse(parts[3]);
                                int targetCount = int.Parse(parts[4]);
                                int currentCount = int.Parse(parts[5]);
                                int bonusPoints = int.Parse(parts[6]);
                                bool isComplete = bool.Parse(parts[7]);

                                var checklist = new ChecklistGoal(name, desc, perEvent, targetCount, bonusPoints);

                                // Apply currentCount times without affecting loadedScore (we'll trust the SCORE line)
                                for (int i = 0; i < currentCount; i++)
                                {
                                    checklist.RecordEvent();
                                }
                                // If the saved SCORE already includes recorded points, we must not double-add.
                                loaded.Add(checklist);
                            }
                            break;

                        case "Negative":
                            // Negative|name|description|penaltyPoints
                            if (parts.Length >= 4)
                            {
                                string name = Unescape(parts[1]);
                                string desc = Unescape(parts[2]);
                                int penalty = int.Parse(parts[3]);
                                loaded.Add(new NegativeGoal(name, desc, penalty));
                            }
                            break;

                        default:
                            Console.WriteLine("Unknown line in save file: " + line);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to parse line: {line}. Error: {ex.Message}");
                }
            }

            _goals = loaded;
            _score = Math.Max(0, loadedScore);
            _level = Math.Max(1, loadedLevel);
            Console.WriteLine($"Loaded {_goals.Count} goals. Score: {_score}. Level: {_level}");
        }

        private string Unescape(string s) => s.Replace("¦", "|");
    }
}

