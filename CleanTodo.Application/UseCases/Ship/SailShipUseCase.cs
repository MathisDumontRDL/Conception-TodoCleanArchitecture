using CleanTodo.Domain.DTOS.Ship;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class SailShipUseCase
{
    private readonly IShipRepository _shipRepository;
    private readonly PortService _portService;

    public SailShipUseCase(IShipRepository shipRepository, PortService portService)
    {
        _shipRepository = shipRepository;
        _portService = portService;
    }

    public async Task<ShipDto> Execute(Guid id, string destination)
    {
        Ship? ship = await _shipRepository.FindById(id);
        if (ship == null)
        {
            throw new NotFoundException(id);
        }
        DockShipDto sailShip = new DockShipDto(ship);
        sailShip.Status = "sailing";
        await _shipRepository.Delete(id);
        try
        {
            await _portService.SailShipAsync(destination, sailShip);
        }
        catch 
        {
            await _shipRepository.Add(ship);
            throw;
        }
        return new ShipDto(ship);
        
       
        
    }
}