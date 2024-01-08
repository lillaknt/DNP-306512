using BlazorToDo.Data;

namespace BlazorToDo.Services;

public interface IToDoService
{
    public void Add(ToDo item);
    public IEnumerable<ToDo> GetAll();

    public void Delete(ToDo item);

    public void Complete(ToDo item);
    public void UnComplete(ToDo item);

    

}