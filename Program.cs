using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Security.AccessControl;
using System.Text.Json;
using TaskTracker;

string[] command = args;
SavedTasks savedTasks = new SavedTasks();

if (command.Length == 0)
{
    Console.WriteLine("Usage: tasktracker [commands]");
    Console.WriteLine("Use 'tasktracker help' to see all available commands");
}
else
{
    switch (command[0])
    {
        case "help":
            listAllCommands();
            break;

        case "add":
            addTask(command);
            break;

        case "update":
            updateTask(command);
            break;

        case "delete":
            deleteTask(command);
            break;

        case "mark-in-progress":
            changeTaskStatus(command, "In-Progress");
            break;

        case "mark-done":
            changeTaskStatus(command, "Done");
            break;
        
        case "mark-todo":
            changeTaskStatus(command, "To do");
            break;
        
        case "list":
            listTasks(args);
            break;

        default:
            Console.WriteLine("Unknown command");
            Console.WriteLine("Use 'tasktracker help' to see all available commands");
            break;
    }
    
}

void addTask(string[] command)
{
    //Verify if the input command matches add operation arguments
    if(command.Length != 2)
    {
        Console.WriteLine("Unknown arguments, please add a name for your task in the following format:");
        Console.WriteLine("add [your description in quotes]");
        return;
    }

    if (string.IsNullOrEmpty(command[1]))
    {
        Console.WriteLine("Invalid task name! Cannot have a blank or null description");
        return;
    }

    savedTasks.Load();
    TaskTracker.Task newTask = new TaskTracker.Task(savedTasks.FileContent.LastId + 1, command[1]);
    savedTasks.AddTask(newTask);
    savedTasks.Save();

    Console.WriteLine($"Task '{newTask.Description}' added successfully.");  
    
}

void updateTask(string[] command)
{

}

void deleteTask(string[] command)
{
    
}

void changeTaskStatus(string[] command, string newStatus)
{
    
}

void listTasks(string[] args)
{
    savedTasks.Load();

    if(savedTasks.IsEmpty())
        Console.WriteLine("Your task list is empty.");
    else
    {
        //Header
        Console.WriteLine("ID\tDescription\tStatus\tCreatedAt\tLastUpdate");
        foreach(var task in savedTasks.FileContent.Tasks)
        {
            Console.WriteLine("");
            Console.Write($"{task.Id}\t{task.Description}\t{task.Status}\t{task.CreatedAt}\t{task.UpdatedAt}");
        }
    }
        
}

void listAllCommands()
{
    
}


