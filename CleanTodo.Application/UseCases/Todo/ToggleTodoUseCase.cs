using CleanTodo.Domain.DTOS.Todo;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class ToggleTodoUseCase
{
    private readonly ITodoRepository _todoRepository;

    public ToggleTodoUseCase(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<TodoDto> Execute(Guid id)
    {
        var todo = await _todoRepository.FindById(id);

        if (todo == null)
        {
            throw new NotFoundException(id);
        }

        todo.IsCompleted = !todo.IsCompleted;

        await _todoRepository.Update(todo);

        return new TodoDto
        {
            Id = todo.Id,
            Title = todo.Text,
            IsCompleted = todo.IsCompleted,
            Date = todo.Date
        };
    }
}