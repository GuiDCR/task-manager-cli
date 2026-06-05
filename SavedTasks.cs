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
    public void Load()
    {
        if (!File.Exists(Path))
        {
            //Creates a new empty json file if not exist
            LoadEmptySave();
            Save();
        }
        else
        {
            try
            {
                string json = File.ReadAllText(Path);
                TaskData? database = JsonSerializer.Deserialize<TaskData>(json);
                
                if(database == null)
                {
                    //Loads an empty save if the existing save file returned null
                    Console.WriteLine("The data found in tasks file was null");
                    Console.WriteLine($"Loading an empty task list");
                    LoadEmptySave();
                }
                else
                {
                    FileContent.LastId = database.LastId;
                    FileContent.Tasks = database.Tasks;
                }
            }
            catch (JsonException ex)
            {
                //If the existing json file not matches json structure an empty save is loaded
                Console.WriteLine($"The existing task file is corrupted!: {ex.Message}");
                Console.WriteLine($"Loading an empty task list");
                LoadEmptySave();
                
            }
  
        }
    }
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

    public void LoadEmptySave()
    {
        FileContent.LastId = 0;
        FileContent.Tasks = new List<Task>();
    }
}