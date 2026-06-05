using System.Text.Json;

namespace TaskTracker;

public class SavedTasks
{
    private const string Path = "./tasks.json";
    public int LastId {get; set; }
    public List<Task> Tasks {get; set; }
    
    public SavedTasks()
    {
        Load(Path);
    }
    public void Load(string path)
    {
        if (!File.Exists(path))
        {
            LastId = 0;
            Tasks = new List<Task>(){};
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText(path, json);
        }
        else
        {
            try
            {
                string json = File.ReadAllText(path);
                SavedTasks? database = JsonSerializer.Deserialize<SavedTasks>(json);
                
                if(database == null)
                {
                    Console.WriteLine("The data found in tasks file was null");
                    Console.WriteLine($"Loading a empty task list");
                    LastId = 0;
                    Tasks = new List<Task>();
                }
                else
                {
                    LastId = database.LastId;
                    Tasks = database.Tasks;
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"The existing task file is invalid!: {ex.Message}");
                Console.WriteLine($"Loading a empty task list");
                LastId = 0;
                Tasks = new List<Task>(){};
            }
  
        }
    }
    public void Save(Task newTask)
    {
        string json = JsonSerializer.Serialize(this);
        File.WriteAllText(Path, json);
    }
}