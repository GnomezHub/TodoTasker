using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoTasker;

namespace ToDoTasker
{
    public class TaskManager
    {
        private const string SaveFile = "tasks_test.json";

        public List<TodoTask> Tasks { get; set; } = [];

        // Returns list of tasks sorted by DueDate
        public IEnumerable<TodoTask> GetTasksSortedByDate()
        {
           return Tasks.OrderBy(t => t.DueDate);
        }
        // Returns list of tasks sorted by project
        public IEnumerable<TodoTask> GetTasksSortedByProject()
        {
            return Tasks.OrderBy(t => t.Project);
        }

        // Returns the total number of tasks marked as Done
        public int GetTotalDoneTasks()
        {
           return Tasks.Count(t => t.Status == TaskStatus.Done);
        }

        // Returns the total number of tasks still Pending
        public int GetTotalPendingTasks()
        {
            return Tasks.Count(t => t.Status == TaskStatus.Todo);
        }

        // adds a new task to the list
        public void AddTask(TodoTask task) 
        {
            Tasks.Add(task);
        }
        //removes a task from the list by index
        public void RemoveTask(int index)
        {
            if (index >= 0 && index < Tasks.Count)
                Tasks.RemoveAt(index);
        }
        // Marks a task as Done by index
        public void MarkTaskDone(int index)
        {
            if (index >= 0 && index < Tasks.Count)
                Tasks[index].Status = TaskStatus.Done;
        }
        //edits a task by index, allowing to change title, due date and project
        public void EditTask(int index, string title, DateTime dueDate, string project)
        {
            if (index >= 0 && index < Tasks.Count)
            {
                Tasks[index].Title = title;
                Tasks[index].DueDate = dueDate;
                Tasks[index].Project = project;
            }
        }

  
        // Attempts to save the current list of tasks to a JSON file and returns true if successful, false otherwise
        public bool doSave()
        {
            //Tries to save the list in a JSON file
            try
            {
                string json = JsonSerializer.Serialize(Tasks, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SaveFile, json);
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        // Attempts to load the list of tasks from a JSON file, if it exists, or creates a sample list if it does not
        public void Load()
        {

            if (File.Exists(SaveFile))
            {
                try
                {
                    var json = File.ReadAllText(SaveFile);
                    Tasks = JsonSerializer.Deserialize<List<TodoTask>>(json) ?? new List<TodoTask>();
                    DecoratedText.BulletLineColored($"Loaded {Tasks.Count} tasks from {SaveFile}", ConsoleColor.Green);
                } catch(Exception) {
                    DecoratedText.BulletLineColored($"failed to load {SaveFile}", ConsoleColor.Red);
                }

            }
            else
            {
                Tasks =
                [
                new TodoTask("Do dishes", "Chores", DateTime.Now.AddDays(2), TaskStatus.Todo),
                new TodoTask("Take out trash", "Chores", DateTime.Now.AddDays(-1), TaskStatus.Done),
                new TodoTask("Read a book", "", DateTime.Now.AddMonths(2), TaskStatus.Todo),
                new TodoTask("Mini Project", "C# .NET", DateTime.Now.AddDays(-6), TaskStatus.Done),
                new TodoTask("Individual Project", "C# .NET", DateTime.Now.AddDays(2), TaskStatus.Todo),
                new TodoTask("HTML & CSS", "C# .NET",  DateTime.Now.AddMonths(1), TaskStatus.Todo)
                ];
                DecoratedText.BulletLineColored("Did not find a saved list to load. Created a sample list with some tasks.", ConsoleColor.Yellow);
            }
        }

    }

}