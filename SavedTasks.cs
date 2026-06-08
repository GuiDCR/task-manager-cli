using System.Data.Common;
using System.Text.Json;

namespace TaskTracker;
public class TaskData
{
    public int LastId {get; set; }
    public List<Task> Tasks {get; set;} = new List<Task>();
}
public class SavedTasks
{
    public string Path {get; set;} = "./savedTasks.json";
    public TaskData FileContent {get; set;}


    public SavedTasks()
    {
        FileContent = new TaskData();
    }

    /*
    Transfer json object atribute values to the current instance
    returns true if load was successful, false case null or corrupted   
    */
    public bool Load()
    {
        if (!File.Exists(Path))
        {
            //Creates a new empty json file if not exist
            LoadEmptySave();
            Save();
            return true;
        }
        else
        { 
            string json = File.ReadAllText(Path);
            TaskData? database = JsonSerializer.Deserialize<TaskData>(json);
            
            if(database == null)
            {
                return false;
            }
            else
            {
                FileContent.LastId = database.LastId;
                FileContent.Tasks = database.Tasks;
                return true;
            }
        }
    }
    
    //Upload the SavedTasks object to json file
    public void Save()
    {
        JsonSerializerOptions options = new JsonSerializerOptions 
        { 
            WriteIndented = true,
            IncludeFields = true
        };
        string json = JsonSerializer.Serialize(FileContent, options);
        File.WriteAllText(Path, json);
    }

    public void AddTask(Task task)
    {
        FileContent.Tasks.Add(task);
        FileContent.LastId = task.Id;
    }

    public void Remove(int id)
    {
        for(int i = 0; i <= FileContent.Tasks.Count() - 1; i++)
        {
            if(FileContent.Tasks[i].Id == id)
            {
                FileContent.Tasks.RemoveAt(i);
                break;       
            }
        }
    }

    public bool IsEmpty()
    {
        return FileContent.Tasks.Count == 0;
    }
    public void LoadEmptySave()
    {
        FileContent.LastId = 0;
        FileContent.Tasks = new List<Task>();
    }

    public void ChangeTaskStatus(int id, string status)
    {
        for(int i = 0; i < FileContent.Tasks.Count; i++)
        {
            if (FileContent.Tasks[i].Id == id)
            {
                FileContent.Tasks[i].Status = status;
                FileContent.Tasks[i].UpdatedAt = DateTime.Now;
                break;
            }
        }
        
    }
}