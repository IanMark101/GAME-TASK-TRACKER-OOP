using System;

namespace GameTaskTracker.Models
{
    // Abstraction: base abstract class for tasks
    public abstract class BaseTask
    {
        public string Id { get; protected set; }
        public string Title { get; set; }

        protected BaseTask() { }

        protected BaseTask(string title)
        {
            Id = Guid.NewGuid().ToString("N").Substring(0, 8);
            Title = title;
        }

        public abstract string Summary();
    }
}
