using CleanTodo.Application.UseCase;
using CleanTodo.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PortController(GetAllPortsUseCase _getAllPortsUseCase) : ControllerBase
{
    [HttpGet("users")]
    public async Task<ActionResult<IEnumerable<string>>> GetAll()
    {
		try
		{
            var ports = await _getAllPortsUseCase.Execute();

            return Ok(ports);
        }
		catch (NotFoundException)
		{

            return NotFound();
		}
       
    }



}
