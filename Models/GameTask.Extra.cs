using System;

namespace GameTaskTracker.Models
{
    // Partial class - part 2
    public partial class GameTask
    {
        public void MarkComplete()
        {
            if (IsComplete) return;
            IsComplete = true;
        }

        public override string ToString()
        {
            return $"[{Id}] {Title} | Type:{Type} | XP:{XP} | {(IsComplete ? "Done" : "Pending")} | Notes:{Notes}";
        }
    }
}
