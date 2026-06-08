using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Security.AccessControl;
using System.Text.Json;
using TaskTracker;

string[] command = args;
string helpMessage = "Use 'tasktracker help' to see all available commands and its arguments";
string loadErrorMessage = "Error: Failed to load tasks file. The file may be corrupted or have invalid content.\nTip: You can delete 'tasks.json' and start fresh, or fix it manually.";

SavedTasks savedTasks = new SavedTasks();

try
{
    if (!savedTasks.Load())
    {
        Console.WriteLine(loadErrorMessage);
        return;    
    }    
}
catch (Exception ex)
{
    //If the existing json file not matches json structure an empty save is loaded
    Console.WriteLine($"{loadErrorMessage}\n{ex.Message}");
    return;         
}

if (command.Length == 0)
{
    Console.WriteLine("Usage: tasktracker [commands]");
    Console.WriteLine(helpMessage);
}
else
{
    switch (command[0])
    {
        case "help":
            listAllCommands();
            break;

        case "add":
            addTask();
            break;

        case "update":
            updateTask();
            break;

        case "delete":
            deleteTask();
            break;

        case "mark-in-progress":
            changeTaskStatus("in-progress");
            break;

        case "mark-done":
            changeTaskStatus("done");
            break;
        
        case "mark-todo":
            changeTaskStatus("todo");
            break;
        
        case "list":
            listTasks();
            break;

        default:
            Console.WriteLine("Unknown command");
            Console.WriteLine(helpMessage);
            break;
    }
    
}

void addTask()
{
    //Verify if the input command matches add operation arguments
    if (!validateArgs(2))
        return;

    if (string.IsNullOrWhiteSpace(command[1]))
    {
        Console.WriteLine("Invalid task name! Cannot have a blank or null description");
        return;
    }
    TaskTracker.Task newTask = new TaskTracker.Task(savedTasks.FileContent.LastId + 1, command[1]);
    savedTasks.AddTask(newTask);
    savedTasks.Save();

    Console.WriteLine($"Task '{newTask.Description}' added successfully (ID: {newTask.Id})");  
    
}

void updateTask()
{
    
}

void deleteTask()
{
    if (!validateArgs(2))
        return;

    if(int.TryParse(command[1], out int id))
    {
        savedTasks.Remove(id);
        savedTasks.Save();
        Console.WriteLine($"Task removed successfully (ID: {id})");
    }
    else
    {
        Console.WriteLine($"'{command[1]}' not represent a valid integer id number");
        Console.WriteLine(helpMessage);   
    }
}

void changeTaskStatus(string newStatus)
{
    if (!validateArgs(2))
        return;

    //Try to convert id string to an integer number if not succeed the program finish 
    if(int.TryParse(command[1], out int id))
    {
        savedTasks.ChangeTaskStatus(id, newStatus);
        savedTasks.Save();
        Console.WriteLine($"Successfully changed task(id {id}) to '{newStatus}'");
    }
    else
    {
        Console.WriteLine($"'{command[1]}' not represent a valid integer id number");
        Console.WriteLine(helpMessage);
    }

}

void listTasks()
{
    if (savedTasks.IsEmpty())
    {
        Console.WriteLine("Your task list is empty.");
        return;
    }

    if (!validateArgs(1, 2))
        return;
    
    if (command.Length == 1)
    {
        listByStatus();
    }
    else
    {
        switch (command[1])
        {
            case "todo":
                listByStatus("todo");
                break;

            case "done":
                listByStatus("done");
                break;
            
            case "in-progress":
                listByStatus("in-progress");
                break;
            
            default:
                Console.WriteLine("Invalid argument for tasktracker list");
                Console.WriteLine(helpMessage);
                break;
        } 
    }   
}


void listByStatus(string status = "")
{
    string header = $"{"ID".PadRight(5)}{"Description".PadRight(20)}{"Status".PadRight(15)}{"CreatedAt".PadRight(25)}{"LastUpdate".PadRight(25)}";
    int counter = 0;

    Console.WriteLine(header);
    foreach(var task in savedTasks.FileContent.Tasks)
    {
        /*If a status is specified in method parameter, only tasks with that status will be printed,
          else all tasks will be printed
        */
        if(task.Status == status || string.IsNullOrEmpty(status))
        {
           printTask(task);
           counter += 1;
        }
    }
    
    // It provides feedback to the user if they have requested to list tasks with a certain status.
    if(counter == 0 && !string.IsNullOrEmpty(status))
        Console.WriteLine($"No task labeled as '{status}' was found");
}

void printTask(TaskTracker.Task task)
{
    Console.WriteLine($"{task.Id.ToString().PadRight(5)}{task.Description.PadRight(20)}{task.Status.PadRight(15)}{task.CreatedAt.ToString().PadRight(25)}{task.UpdatedAt.ToString().PadRight(25)}");
}

void listAllCommands()
{
    
}

bool validateArgs(params int[] validLengths)
{
    if (validLengths.Contains(command.Length))
        return true;

    Console.WriteLine($"Invalid arguments for '{command[0]}'.");
    Console.WriteLine(helpMessage);
    return false;
}
