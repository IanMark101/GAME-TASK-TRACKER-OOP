using System;
using System.Linq;
using GameTaskTracker.Models;
using GameTaskTracker.Services;
using GameTaskTracker.Repositories;

namespace GameTaskTracker
{
    class Program
    {
        static Repository<GameTask> repo = new Repository<GameTask>("tasks.json");
        static TaskManager manager = new TaskManager(repo);
        static NotificationService notifier = new NotificationService();

        static void Main(string[] args)
        {
            // Subscribe to TaskCompleted event using lambda and method (demonstrates delegates/events)
            manager.TaskCompleted += (s, e) => Console.WriteLine($"[LAMBDA] Congrats! '{e.Task.Title}' rewarded {e.Task.XP} XP.");
            manager.TaskCompleted += notifier.OnTaskCompleted;

            // Load saved tasks (file I/O + exception handling inside repo)
            repo.Load();

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=== Gaming Task Tracker ===");
                Console.WriteLine("1. Add Quest");
                Console.WriteLine("2. View Quests");
                Console.WriteLine("3. Complete Quest");
                Console.WriteLine("4. Save");
                Console.WriteLine("5. Exit");
                Console.Write("Choose: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddQuest();
                        break;
                    case "2":
                        ViewQuests();
                        break;
                    case "3":
                        CompleteQuest();
                        break;
                    case "4":
                        repo.Save();
                        Console.WriteLine("Saved to tasks.json");
                        break;
                    case "5":
                        repo.Save();
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }

            Console.WriteLine("Exiting. Good luck in game!");
        }

        static void AddQuest()
        {
            Console.Write("Title: ");
            var title = Console.ReadLine();

            Console.Write("Type (Daily/Weekly/Event): ");
            var typeInput = Console.ReadLine();
            TaskType type = TaskType.Daily;
            if (!Enum.TryParse(typeInput, true, out type)) type = TaskType.Daily;

            Console.Write("Notes (optional): ");
            var notes = Console.ReadLine();

            Console.Write("XP reward (number): ");
            if (!int.TryParse(Console.ReadLine(), out int xp)) xp = 10;

            var t = new GameTask(title, type, notes, xp);
            manager.AddTask(t);
            Console.WriteLine($"Quest added. ID: {t.Id}");
        }

        static void ViewQuests()
        {
            var all = repo.GetAll();

            // Example lambda: list pending tasks only if user wants
            Console.Write("Show only pending? (y/n): ");
            var onlyPending = Console.ReadLine()?.Trim().ToLower() == "y";
            var list = onlyPending ? all.Where(t => !t.IsComplete).ToList() : all.ToList();

            if (!list.Any())
            {
                Console.WriteLine("No quests found.");
                return;
            }

            foreach (var q in list)
                Console.WriteLine(q);
        }

        static void CompleteQuest()
        {
            Console.Write("Enter Quest ID to complete: ");
            var id = Console.ReadLine();
            if (manager.CompleteTask(id))
                Console.WriteLine("Quest marked complete.");
            else
                Console.WriteLine("Quest not found.");
        }
    }
}
