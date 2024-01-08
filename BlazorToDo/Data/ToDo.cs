namespace BlazorToDo.Data;

public class ToDo
{
    //text property is initialized, completed property is not, therefor it is default boolean: false
    public ToDo(string text)
    {
        Text = text;
    }

    public string Text { get; set; }
    public bool Completed { get; set; }
}