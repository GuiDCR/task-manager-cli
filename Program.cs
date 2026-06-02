using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Security.AccessControl;
using System.Text.Json;


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
            addTask(args);
            break;

        case "update":
            updateTask(args);
            break;

        case "delete":
            deleteTask(args);
            break;

        case "mark-in-progress":
            changeTaskStatus(args, "In-Progress");
            break;

        case "mark-done":
            changeTaskStatus(args, "Done");
            break;
        
        case "mark-todo":
            changeTaskStatus(args, "To do");
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

    if(command.Length != 2)
    {
        Console.WriteLine("Insuficient arguments, please add a name for your task in the following format:");
        Console.WriteLine("add [your description in quotes]");
        return;
    }

    TaskTracker.Task newTask = new TaskTracker.Task(generateNewId(), command[1]);  
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

int generateNewId()
{
    return 0;
}



