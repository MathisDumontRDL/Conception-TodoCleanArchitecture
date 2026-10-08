using CleanTodo.Application.UseCase;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.DTOS.Ship;
using CleanTodo.Domain.DTOS.Todo;
using CleanTodo.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ShipController(CreateShipUseCase _createdShipUseCase, GetShipUseCase _getShipUseCase, GetAllShipsUseCase _getAllShipsUseCase, DeleteShipUseCase _deleteShipUseCase, UpdateShipUseCase _updateShipUseCase) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShipDto>>> GetAll()
    {
        var ships = await _getAllShipsUseCase.Execute();
        return Ok(ships);
    }

    [HttpGet("{id}")] // /api/todo/ton_id
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            ShipDto ship = await _getShipUseCase.Execute(id);
            return Ok(ship);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("Add")]
    public async Task<ActionResult<ShipDto>> Create([FromBody] CreateShipDto createShipDto)
    {
        ShipDto ship = await _createdShipUseCase.Execute(createShipDto);

        return CreatedAtAction(
            nameof(Get),
            new { id = ship.Id },
            ship);
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteShipUseCase.Execute(id);
            return NoContent();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPatch("{id}/update")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ModifyShipDto modifyShipDto)
    {
        try
        {
            var todo = await _updateShipUseCase.Execute(id, modifyShipDto);
            return NoContent();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

}
