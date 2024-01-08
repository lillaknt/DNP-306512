using BlazorToDo.Data;

namespace BlazorToDo.Services;

public class ToDoService : IToDoService
{
    private readonly IList<ToDo> toDoItems;

    public ToDoService()
    {
        toDoItems = new List<ToDo>
        {
            new ToDo("Pass DNP with a 12"),
            new ToDo("Celebrate 12")
        };
    }
    
    public void Add(ToDo item)
    {
       toDoItems.Add(item);
    }

    public IEnumerable<ToDo> GetAll()
    {
        return toDoItems.ToList();
    }

    public void Delete(ToDo item)
    {
        toDoItems.Remove(item);
    }

    public void Complete(ToDo item)
    {
        item.Completed = true;
    }

    public void UnComplete(ToDo item)
    {
        item.Completed = false;

    }
}