using System;

namespace TaskTracker;

public class Task
{   public int Id { get; set; }
    public string Description { get; set; }
    public string Status { get; set;}
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Task(int id, string name)
    {
        Id = id;
        Description = name;
        Status = "todo";
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
   
    public void Update(int id)
    {
        
    }
}