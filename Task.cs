using System;

namespace TaskTracker;

public class Task
{   public int id;
    public string description { get; set; }
    public string status { get; set;}
    public DateTime createdAt;
    public DateTime updatedAt;

    public Task(int id, string name)
    {
        this.id = id;
        this.description = name;
        this.status = "todo";
        this.createdAt = DateTime.Now;
        this.updatedAt = DateTime.Now;
    }
   
}