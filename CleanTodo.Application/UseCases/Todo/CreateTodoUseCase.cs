using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class CreateTodoUseCase
{
    private readonly ITodoRepository _todoRepository;

    public CreateTodoUseCase(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;

    }

    public async Task<TodoDto> Execute(CreateTodoDto createdTodo)
    {
        Todo todoConvert = new Todo(createdTodo.Title);

        Todo todo = await _todoRepository.Add(todoConvert);
        //validation et le return?
        return new TodoDto(todo);
    }
}