using System;
using GameTaskTracker.Events;

namespace GameTaskTracker.Services
{
    public class NotificationService
    {
        public void OnTaskCompleted(object sender, TaskEventArgs e)
        {
            Console.WriteLine($"[NOTIFIER] Achievement unlocked for completing: {e.Task.Title} (+{e.Task.XP} XP)");
        }
    }
}
