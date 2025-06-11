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
     Load and save task list to file The solution may also include other creative features at your discretion in case you wish to show some flair. 


   steps in development of the app:

1. Define the `TodoTask` class with properties for title, due date, status, and project.
2. Create the `TaskManager` class to manage a list of tasks, including methods for adding, removing, editing, marking as done.
3. Implement methods in `TaskManager` to sort tasks by date and project.
4. Use JSON serialization for saving and loading tasks to/from a file.
5. Create a console application with a menu-driven interface to interact with the user.
    5.1  methods for displaying bullet points and numbered lists to match mockup.
    5.2  add submenus to main menu for show lists [by date, by project], editing [adding, updating, marking as done, remove], saving&quit.
*/

using System.Globalization;
using ToDoTasker;
using TaskStatus = ToDoTasker.TaskStatus;

TaskManager manager = new TaskManager();
manager.Load();
bool firstRun = true;
while (true)
{
    // Show status of loaded tasks on first run
    if (firstRun) { firstRun = false; } else { Console.Clear(); }

    DecoratedText.BulletLine("Welcome to ToDoTasker!");
    int pending = manager.GetTotalPendingTasks();
    int done = manager.GetTotalDoneTasks();
    string pendingText = pending == 1 ? "task to do" : "tasks to do";
    string doneText = done == 1 ? "task is done" : "tasks are done";
    DecoratedText.Bullet();
    Console.Write($"You have {pending} {pendingText} and {done} {doneText}");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("!");
    DecoratedText.BulletLine("ToDoTasker - Main Menu");

    DecoratedText.BulletNumLine(1, "Show task list");
    DecoratedText.BulletNumLine(2, "Add new task");
    DecoratedText.BulletNumLine(3, "Edit task");
    DecoratedText.BulletNumLine(4, "Save and Quit");
    DecoratedText.Bullet();
    Console.Write("pick an option: ");
    Console.ForegroundColor = ConsoleColor.Cyan;
    var input = Console.ReadLine();
    Console.ResetColor();

    switch (input)
    {
        case "1":
            // Show task list submenu
            Console.Clear();
            DecoratedText.BulletLine("Show Task List");
            DecoratedText.BulletNumLine(1, "By date");
            DecoratedText.BulletNumLine(2, "By project");
            DecoratedText.Bullet();
            Console.Write("pick an option: ");
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
                    DecoratedText.BulletLineColored("Invalid option.", ConsoleColor.Red);
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
            DecoratedText.BulletLine("Edit Task");
            DecoratedText.BulletNumLine(1, "Update task");
            DecoratedText.BulletNumLine(2, "Mark task as done");
            DecoratedText.BulletNumLine(3, "Remove task");
            DecoratedText.Bullet();
            Console.Write("pick an option: ");
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
                    DecoratedText.BulletLineColored("Invalid option.", ConsoleColor.Red);
                    Pause();
                    break;
            }
            break;
        case "4":
            if (manager.doSave())
            {
                DecoratedText.BulletLineColored("Tasks saved. Goodbye!", ConsoleColor.Green);
            }
            else
            {
                DecoratedText.BulletLineColored("Failed to save tasks.Bye.", ConsoleColor.Red);

            }
            return;
        default:
            DecoratedText.BulletLineColored("Invalid option.", ConsoleColor.Red);
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
        DecoratedText.BulletLineColored("No tasks found.", ConsoleColor.Yellow);

}

static void AddTask(TaskManager manager)
{
    DecoratedText.Bullet();
    Console.Write("Title: ");
    var title = Console.ReadLine();

    DecoratedText.Bullet();
    Console.Write("Project: ");
    var project = Console.ReadLine();

    DecoratedText.Bullet();
    Console.Write("Due date (yyyy-MM-dd): ");
    // var dueDateStr = Console.ReadLine();
    DateTime dueDate;

    while (!DateTime.TryParse(Console.ReadLine(), out dueDate))
    {
        DecoratedText.BulletLineColored("Invalid date format.", ConsoleColor.Red);
        DecoratedText.Bullet();
        Console.Write("Due date (yyyy-MM-dd): ");
    }
    manager.AddTask(new TodoTask(title, project, dueDate, TaskStatus.Todo));
    DecoratedText.BulletLineColored("Task added.", ConsoleColor.Green);
    Pause();
}

static void EditTask(TaskManager manager)
{
    ListTasks(manager.Tasks);
    DecoratedText.Bullet();
    Console.Write("Task number to edit: ");
    if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= manager.Tasks.Count)
    {
        TodoTask task = manager.Tasks[idx - 1];//used non zero based index for user friendliness
        DecoratedText.Bullet();
        Console.Write($"New title (leave blank to keep '{task.Title}'): ");
        DecoratedText.Bullet();
        var title = Console.ReadLine();
        DecoratedText.Bullet();
        Console.Write($"New project (leave blank to keep '{task.Project}'): ");
        var project = Console.ReadLine();
        DecoratedText.Bullet();
        Console.Write($"New due date (yyyy-MM-dd, leave blank to keep '{task.DueDate:yyyy-MM-dd}'): ");
        var dueDateStr = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(title))
            task.Title = title;
        if (!string.IsNullOrWhiteSpace(dueDateStr) &&
            DateTime.TryParseExact(dueDateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dueDate))
            task.DueDate = dueDate;
        if (!string.IsNullOrWhiteSpace(project))
            task.Project = project;
        DecoratedText.BulletLineColored("Task updated.", ConsoleColor.Green);
    }
    else
    {
        DecoratedText.BulletLineColored("Invalid selection.", ConsoleColor.Red);
    }
    Pause();
}

static void MarkTaskDone(TaskManager manager)
{
    ListTasks(manager.Tasks);
    DecoratedText.Bullet();
    Console.Write("Task number to mark as done: ");
    if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= manager.Tasks.Count)
    {
        manager.MarkTaskDone(idx - 1);//used non zero based index for user friendliness
        DecoratedText.BulletLineColored("Task marked as done.", ConsoleColor.Green);
    }
    else
    {
        DecoratedText.BulletLineColored("Invalid selection.", ConsoleColor.Red);
    }
    Pause();
}

static void RemoveTask(TaskManager manager)
{
    ListTasks(manager.Tasks);
    DecoratedText.Bullet();
    Console.Write("Task number to remove: ");
    if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= manager.Tasks.Count)
    {
        manager.RemoveTask(idx - 1);//used non zero based index for user friendliness
        DecoratedText.BulletLineColored("Task removed.", ConsoleColor.Green);
    }
    else
    {
        DecoratedText.BulletLineColored("Invalid selection.", ConsoleColor.Red);
    }
    Pause();
}

static void Pause()
{
    DecoratedText.Bullet();
    Console.Write("Press any key to continue...");
    Console.ReadKey();
}
