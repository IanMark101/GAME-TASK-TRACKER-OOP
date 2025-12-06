using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace GameTaskTracker.Repositories
{
    // Generic repository
    public class Repository<T> where T : class
    {
        private readonly string _filePath;
        private List<T> _items = new List<T>();

        public Repository(string filePath)
        {
            _filePath = filePath;
        }

        public void Add(T item) => _items.Add(item);

        public IReadOnlyList<T> GetAll() => _items.AsReadOnly();

        public bool RemoveById(string id)
        {
            var item = _items.Find(i =>
            {
                var prop = i.GetType().GetProperty("Id");
                if (prop == null) return false;
                var val = prop.GetValue(i)?.ToString();
                return val == id;
            });

            if (item == null) return false;
            _items.Remove(item);
            return true;
        }

        public void Save()
        {
            try
            {
                var json = JsonSerializer.Serialize(_items, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to {_filePath}: {ex.Message}");
            }
        }

        public void Load()
        {
            try
            {
                if (!File.Exists(_filePath)) return;
                var json = File.ReadAllText(_filePath);
                _items = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading {_filePath}: {ex.Message}");
                _items = new List<T>();
            }
        }
    }
}
