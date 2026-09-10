using CleanTodo.Domain.Entities;

namespace CleanTodo.Domain.DTOS;

public class CreateTodoDto
{
    public string Title { get; set; }
    public DateTime Date { get; set; }

    public CreateTodoDto() { }


    // Devrait être fait dans Mapping -> automapper.
    public CreateTodoDto(Todo todo)
    {
        Title = todo.Text;
        Date = todo.Date;
    }
}
