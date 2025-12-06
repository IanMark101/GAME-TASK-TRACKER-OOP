using System.Linq;
using GameTaskTracker.Models;
using GameTaskTracker.Repositories;
using GameTaskTracker.Events;

namespace GameTaskTracker.Services
{
    public class TaskManager
    {
        private readonly Repository<GameTask> _repo;

        // Delegate & Event for completed tasks
        public delegate void TaskCompletedHandler(object sender, TaskEventArgs e);
        public event TaskCompletedHandler TaskCompleted;

        public TaskManager(Repository<GameTask> repo)
        {
            _repo = repo;
        }

        public void AddTask(GameTask task)
        {
            _repo.Add(task);
        }

        public bool CompleteTask(string id)
        {
            // lambda used to find task
            var task = _repo.GetAll().FirstOrDefault(t => {
                var prop = t.GetType().GetProperty("Id");
                return prop != null && prop.GetValue(t)?.ToString() == id;
            });

            if (task == null) return false;

            task.MarkComplete();

            // raise event
            TaskCompleted?.Invoke(this, new TaskEventArgs(task));
            return true;
        }
    }
}
