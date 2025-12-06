using System;

namespace GameTaskTracker.Models
{
    // Partial class - part 1
    public partial class GameTask : BaseTask
    {
        public TaskType Type { get; set; }
        public string Notes { get; set; }
        public int XP { get; set; }
        public bool IsComplete { get; private set; }
        public DateTime CreatedAt { get; set; }

        // Parameterless constructor for deserialization
        public GameTask() { }

        public GameTask(string title, TaskType type, string notes, int xp) : base(title)
        {
            Type = type;
            Notes = notes;
            XP = xp;
            IsComplete = false;
            CreatedAt = DateTime.Now;
        }

        public override string Summary()
        {
            return $"{Title} [{Type}] - XP:{XP} - {(IsComplete ? "Completed" : "Pending")}";
        }
    }
}
