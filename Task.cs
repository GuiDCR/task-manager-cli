using System;

namespace TaskTracker;

public class Task
{   public int Id { get; set; }
    public string Description { get; set; } = "";
    public string Status { get; set;} = "";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    //Empty task constructor necessary to JsonDeserializer work
    public Task()
    {
    }
    public Task(int id, string description)
    {
        Id = id;
        Description = description;
        Status = "todo";
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
}