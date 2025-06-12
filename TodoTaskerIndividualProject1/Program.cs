/*
Project Name: ToDoTasker

//Assignment description:
Your task is to build a todo list application. The application will allow a user to create new tasks, 
assign them a title and due date, and choose a project for that task to belong to. They will need 
to use a text based user interface via the command-line. Once they are using the application,
the user should be able to also edit, mark as done or remove tasks. They can also quit and save 
the current task list to file, and then restart the application with the former state restored.:  

//Requirements
//The solution must achieve the following requirements:  

     Model a task with a task title, due date, status and project  
     Display a collection of tasks that can be sorted both by date and project  
     Support the ability to add, edit, mark as done, and remove tasks  
     Support a text-based user interface
     Load and save task list to file 


   steps in development of the app:

1. Define the `TodoTask` class with properties for title, due date, status, and project.
2. Create the `TaskManager` class to manage a list of tasks, including methods for adding, removing, editing, marking as done.
3. Implement methods in `TaskManager` to sort tasks by date and project.
4. Create a console application with a menu-driven interface to interact with the user.
5. Use JSON serialization for saving and loading tasks to/from a file.
6. Further interface design (Methods for displaying bullet points and numbered lists to match mockup.
   + Add submenus to main menu for show lists [by date, by project], editing [adding, updating, marking as done, remove], saving&quit.)
7. Implement error handling for user inputs and file operations.
*/

using Microsoft.VisualBasic;
using System.Globalization;
using ToDoTasker;
using TaskStatus = ToDoTasker.TaskStatus;

TaskManager manager = new TaskManager("tasks_test66.json");

bool firstRun = true;
while (true)
{
    // Show status of loaded tasks on first run
    if (firstRun) { firstRun = false; } else { Console.Clear(); }

    DecoratedText.WriteLine("Welcome to ToDoTasker!");
    int pending = manager.GetTotalPendingTasks();
    int done = manager.GetTotalDoneTasks();
    string pendingText = pending == 1 ? "task to do" : "tasks to do";
    string doneText = done == 1 ? "task is done" : "tasks are done";
    DecoratedText.Write($"You have {pending} {pendingText} and {done} {doneText}");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("!");

    DecoratedText.WriteLine("ToDoTasker - Main Menu");
    DecoratedText.WriteLine("Pick an option:");
    DecoratedText.WriteLine(1, "Show Task List (by date or project)");
    DecoratedText.WriteLine(2, "Add New Task");
    DecoratedText.WriteLine(3, "Edit Task (update, mark as done, remove)");
    DecoratedText.WriteLine(4, "Save and Quit");
    DecoratedText.Write("Your option: ");
    Console.ForegroundColor = ConsoleColor.Cyan;
    var input = Console.ReadLine();
    Console.ResetColor();

    switch (input)
    {
        case "1":
            // Show task list submenu
            Console.Clear();
            DecoratedText.WriteLine("Show Task List");
            DecoratedText.WriteLine(1, "By date");
            DecoratedText.WriteLine(2, "By project");
            DecoratedText.Write("pick an option: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            var listInput = Console.ReadLine();
            Console.ResetColor();
            switch (listInput)
            {
                case "1":
                    ListTasks(manager.GetTasksSortedByDate());
                    break;
                case "2":
                    ListTasks(manager.GetTasksSortedByProject());
                    break;
                default:
                    DecoratedText.WriteLine("Invalid option.", ConsoleColor.Red);
                    break;
            }
            Pause();
            break;
        case "2":
            AddTask(manager);
            break;
        case "3":
            // Edit task submenu
            Console.Clear();
            DecoratedText.WriteLine("Edit Task");
            DecoratedText.WriteLine(1, "Update task");
            DecoratedText.WriteLine(2, "Mark task as done");
            DecoratedText.WriteLine(3, "Remove task");
            DecoratedText.Write("pick an option: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            var editInput = Console.ReadLine();
            Console.ResetColor();
            switch (editInput)
            {
                case "1":
                    EditTask(manager);
                    break;
                case "2":
                    MarkTaskDone(manager);
                    break;
                case "3":
                    RemoveTask(manager);
                    break;
                default:
                    DecoratedText.WriteLine("Invalid option.", ConsoleColor.Red);
                    Pause();
                    break;
            }
            break;
        case "4":
            if (manager.doSave())
            {
                DecoratedText.WriteLine("Tasks saved. Goodbye!", ConsoleColor.Green);
            }
            else
            {
                DecoratedText.WriteLine("Failed to save tasks.Bye.", ConsoleColor.Red);

            }
            return;
        default:
            DecoratedText.WriteLine("Invalid option.", ConsoleColor.Red);
            Pause();
            break;
    }
}


static void ListTasks(IEnumerable<TodoTask> tasks)
{
    Console.WriteLine();
    int padding = 20;
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("Task".PadRight(padding/2) + "Title".PadRight(padding) + "Project".PadRight(padding) + "Due date".PadRight(padding) + "Status");
    Console.ResetColor();
    int i = 1;//used non zero based index for user friendliness
    foreach (var task in tasks)
    {
        Console.Write($"({i++})".PadRight(padding/2));
        Console.Write(task.Title.PadRight(padding) + task.Project.PadRight(padding));
        Console.Write($"{task.DueDate:yyyy-MM-dd}".PadRight(padding));
        if (task.Status == TaskStatus.Done)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[Done]");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[Todo]");

        }
        Console.ResetColor();
        Console.WriteLine();
    }
    Console.WriteLine();
    if (i == 1)
        DecoratedText.WriteLine("No tasks found.", ConsoleColor.Yellow);

}

static void AddTask(TaskManager manager)
{
    DecoratedText.Write("Title: ");
    var title = Console.ReadLine();

    DecoratedText.Write("Project: ");
    var project = Console.ReadLine();

    DecoratedText.Write("Due date (yyyy-MM-dd): ");
  
    DateTime dueDate;

    while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dueDate))
    {
        DecoratedText.WriteLine("Invalid date format. Should be (yyyy-MM-dd)", ConsoleColor.Red);
        DecoratedText.Write("Due date (yyyy-MM-dd): ");
    }
    manager.AddTask(new TodoTask(title, project, dueDate, TaskStatus.Todo));
    DecoratedText.WriteLine("Task added.", ConsoleColor.Green);
    Pause();
}

static void EditTask(TaskManager manager)
{
    ListTasks(manager.Tasks);
    DecoratedText.Write("Task number to edit: ");
    if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= manager.Tasks.Count)
    {
        TodoTask task = manager.Tasks[idx - 1];//used non zero based index for user friendliness
        DecoratedText.Write($"New title (leave blank to keep '{task.Title}'): ");
        var title = Console.ReadLine();
        DecoratedText.Write($"New project (leave blank to keep '{task.Project}'): ");
        var project = Console.ReadLine();
        DecoratedText.Write($"New due date (yyyy-MM-dd, leave blank to keep '{task.DueDate:yyyy-MM-dd}'): ");
        var dueDateStr = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(title))
            task.Title = title;
        if (!string.IsNullOrWhiteSpace(project))
            task.Project = project;
        if (!string.IsNullOrWhiteSpace(dueDateStr) &&
            DateTime.TryParseExact(dueDateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dueDate))
            task.DueDate = dueDate;
   
        DecoratedText.WriteLine("Task updated.", ConsoleColor.Green);
    }
    else
    {
        DecoratedText.WriteLine("Invalid selection.", ConsoleColor.Red);
    }
    Pause();
}

static void MarkTaskDone(TaskManager manager)
{
    ListTasks(manager.Tasks);
    DecoratedText.Write("Task number to mark as done: ");
    if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= manager.Tasks.Count)
    {
        manager.MarkTaskDone(idx - 1);//used non zero based index for user friendliness
        DecoratedText.WriteLine("Task marked as done.", ConsoleColor.Green);
    }
    else
    {
        DecoratedText.WriteLine("Invalid selection.", ConsoleColor.Red);
    }
    Pause();
}

static void RemoveTask(TaskManager manager)
{
    ListTasks(manager.Tasks);
    DecoratedText.Write("Task number to remove: ");
    if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= manager.Tasks.Count)
    {
        manager.RemoveTask(idx - 1);//used non zero based index for user friendliness
        DecoratedText.WriteLine("Task removed.", ConsoleColor.Green);
    }
    else
    {
        DecoratedText.WriteLine("Invalid selection.", ConsoleColor.Red);
    }
    Pause();
}

static void Pause()
{
    DecoratedText.Write("Press any key to continue...");
    Console.ReadKey();
}
