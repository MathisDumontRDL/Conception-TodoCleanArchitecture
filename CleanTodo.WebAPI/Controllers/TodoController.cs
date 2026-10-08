using CleanTodo.Application.UseCase;
using CleanTodo.Domain.DTOS.Todo;
using CleanTodo.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TodoController(GetAllTodosUseCase getAllUseCase, GetTodoUseCase getTodoUseCase, CreateTodoUseCase _createUseCase, ToggleTodoUseCase _toggleTodoUseCase, DeleteTodoUseCase _deleteTodoUseCase) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoDto>>> GetAll()
    {
        var todos = await getAllUseCase.Execute();
        return Ok(todos);
    }

    //Cadeau! pour le create. On utilise un CreatedAtAction qui retourne un code http 201 et un header location avec l'url du nouvel élément créé.

    [HttpPost("Add")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<TodoDto>> Create([FromBody] CreateTodoDto createTodoDto)
    {
        TodoDto todo = await _createUseCase.Execute(createTodoDto);

        return CreatedAtAction(
            nameof(Get),
            new { id = todo.Id },
            todo);
    }

    [HttpGet("{id}")] // /api/todo/ton_id
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            TodoDto todo = await getTodoUseCase.Execute(id);
            return Ok(todo);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }


    [HttpPatch("{id}/toggle")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Toggle(Guid id)
    {
        try
        {
            var todo = await _toggleTodoUseCase.Execute(id);
            return NoContent();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteTodoUseCase.Execute(id);
            return NoContent();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    // Pour le delete et le update, tu peux retourn un noContent (http 204) qui dit :"Ça fonctionné, je n'ai rien à te retourner"
    //return NoContent();
}
