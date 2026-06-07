using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Security.AccessControl;
using System.Text.Json;
using TaskTracker;

string[] command = args;
string helpMessage = "Use 'tasktracker help' to see all available commands and its arguments";
SavedTasks savedTasks = new SavedTasks();


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
            Console.WriteLine(helpMessage);
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

    Console.WriteLine($"Task '{newTask.Description}' added successfully (ID: {newTask.Id})");  
    
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

    if (savedTasks.IsEmpty())
    {
        Console.WriteLine("Your task list is empty.");
        return;
    }

    if(command.Length > 2)
    {
        Console.WriteLine("Invalid argument for tasktracker list");
        Console.WriteLine(helpMessage);
    }
    else if (command.Length == 1)
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


