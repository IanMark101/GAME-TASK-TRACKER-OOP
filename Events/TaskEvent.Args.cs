using System;
using GameTaskTracker.Models;

namespace GameTaskTracker.Events
{
    public class TaskEventArgs : EventArgs
    {
        public GameTask Task { get; }
        public TaskEventArgs(GameTask task) => Task = task;
    }
}
