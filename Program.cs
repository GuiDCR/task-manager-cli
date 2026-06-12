using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Security.AccessControl;
using System.Text.Json;
using TaskTracker;

string[] command = args;
const int maxDescLength = 40;
string helpMessage = "Use 'tasktracker help' to see all available commands and its arguments";
string loadErrorMessage = "Error: Failed to load tasks file. The file may be corrupted or have invalid content.\nTip: You can delete 'tasks.json' and start fresh, or fix it manually.";

SavedTasks savedTasks = new SavedTasks();


if (!savedTasks.Load())
{
    Console.WriteLine(loadErrorMessage);
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
        Console.WriteLine("Invalid task description! Cannot have a blank or null description");
        return;
    }

    if(command[1].Length > maxDescLength)
    {
        Console.WriteLine("The task description cannot exceed 40 characters.");
        return;
    }

    TaskTracker.Task newTask = new TaskTracker.Task(savedTasks.FileContent.LastId + 1, command[1]);
    savedTasks.AddTask(newTask);
    savedTasks.Save();

    Console.WriteLine($"Task '{newTask.Description}' added successfully (ID: {newTask.Id})");  
    
}

void updateTask()
{
    if(!validateArgs(3))
        return;

    if (string.IsNullOrWhiteSpace(command[2]))
    {
        Console.WriteLine("Invalid task name! Cannot have a blank or null description");
        return; 
    }

    if(!int.TryParse(command[1], out int id))
    {
        Console.WriteLine($"'{command[1]}' not represent a valid integer id number");
        Console.WriteLine(helpMessage);
        return;
    }

    if(savedTasks.Update(id, command[2]))
    {
        savedTasks.Save();
        Console.WriteLine($"Task updated successfully (ID: {id})");
    }
    else
    {
        Console.WriteLine($"Task (ID: {id}) not found");
    }
}
        

void deleteTask()
{
    if (!validateArgs(2))
        return;

    if(!int.TryParse(command[1], out int id))
    {
        Console.WriteLine($"'{command[1]}' not represent a valid integer id number");
        Console.WriteLine(helpMessage);  
        return; 
    }

    if (savedTasks.Remove(id))
    {
        savedTasks.Save();
        Console.WriteLine($"Task removed successfully (ID: {id})");
    }
    else
    {
        Console.WriteLine($"Task (ID: {id}) not found");
    }
}

void changeTaskStatus(string newStatus)
{
    if (!validateArgs(2))
        return;

   if(!int.TryParse(command[1], out int id))
    {
        Console.WriteLine($"'{command[1]}' not represent a valid integer id number");
        Console.WriteLine(helpMessage);  
        return; 
    }

    if (savedTasks.ChangeTaskStatus(id, newStatus))
    {
        savedTasks.Save();
        Console.WriteLine($"Successfully changed task(id {id}) to '{newStatus}'");
    }
    else
    {
        Console.WriteLine($"Task (ID: {id}) not found");
    }

}



void listTasks()
{

    if (!validateArgs(1, 2))
        return;
    
    if (savedTasks.IsEmpty())
    {
        Console.WriteLine("Your task list is empty.");
        return;
    }

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
    string header = $"{"ID".PadRight(5)}{"Description".PadRight(maxDescLength + 5)}{"Status".PadRight(15)}{"CreatedAt".PadRight(25)}{"LastUpdate".PadRight(25)}";
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
    Console.WriteLine($"{task.Id.ToString().PadRight(5)}{task.Description.PadRight(maxDescLength + 5)}{task.Status.PadRight(15)}{task.CreatedAt.ToString().PadRight(25)}{task.UpdatedAt.ToString().PadRight(25)}");
}

void listAllCommands()
{
    Console.WriteLine(@"
    TaskTracker-CLI 1.0.0
    An cli app that manages and track your to-do tasks
    Built by: GuiDCR - https://github.com/GuiDCR
    Project proposed by roadmap.sh

    Usage: tasktracker <command> [<args>]

    Avaiable commands:
        add <description in quotes>         Add a new task 
        delete <id>                         Delete a task
        update <id> <description in quotes> Update a task description
        list                                List all tasks
        list <status>                       List tasks by status (todo, in-progress, done)
        mark-todo <id>                      Mark a task as todo
        mark-done <id>                      Mark a task as done
        mark-in-progress <id>               Mark a task as in-progress
        help                                Show all known commands
    ");
}

bool validateArgs(params int[] validLengths)
{
    if (validLengths.Contains(command.Length))
        return true;

    Console.WriteLine($"Invalid arguments for '{command[0]}'.");
    Console.WriteLine(helpMessage);
    return false;
}



