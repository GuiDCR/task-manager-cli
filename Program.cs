using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Security.AccessControl;
using System.Text.Json;
using TaskTracker;

string[] command = args;

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

    SavedTasks savedTasks = new SavedTasks();
    TaskTracker.Task newTask = new TaskTracker.Task(savedTasks.LastId + 1, command[1]);

    savedTasks.LastId = newTask.Id;
    savedTasks.Tasks.Add(newTask);
    savedTasks.Save(newTask);

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
    
}

void listAllCommands()
{
    
}


