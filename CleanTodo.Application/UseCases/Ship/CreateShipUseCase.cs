using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.DTOS.Ship;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class CreateShipUseCase
{
    private readonly IShipRepository _shipRepository;

    public CreateShipUseCase(IShipRepository shipRepository)
    {
        _shipRepository = shipRepository;
    }

    public async Task<ShipDto> Execute(CreatedShipDto createdShip)
    {
        Ship shipConvert = new Ship(createdShip.Name, createdShip.GoldCargo, createdShip.Captain, createdShip.Status, createdShip.CrewSize, createdShip.CreatedBy);

        Ship ship = await _shipRepository.Add(shipConvert);
        return new ShipDto(ship);
    }
}