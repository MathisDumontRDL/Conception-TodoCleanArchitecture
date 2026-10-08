using CleanTodo.Domain.DTOS.Ship;
using CleanTodo.Domain.DTOS.Todo;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class GetAllShipsUseCase
{
    private readonly IShipRepository _shipRepository;

    public GetAllShipsUseCase(IShipRepository shipRepository)
    {
        _shipRepository = shipRepository;
    }

    public async Task<IList<ShipDto>> Execute()
    {
        var ships = await _shipRepository.GetAll();
        return ships.Select(x => new ShipDto(x)).ToList();
    }
}