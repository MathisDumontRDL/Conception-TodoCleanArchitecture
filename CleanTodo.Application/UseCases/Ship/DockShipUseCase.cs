using CleanTodo.Domain.DTOS.Ship;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Interfaces.Repositories;
using FluentValidation;
namespace CleanTodo.Application.UseCase;

public class DockShipUseCase
{
    private readonly IShipRepository _shipRepository;
    private readonly IValidator<DockShipDto> _validator;


    public DockShipUseCase(IShipRepository shipRepository, IValidator<DockShipDto> validator)
    {
        _shipRepository = shipRepository;
        _validator = validator;
    }

    public async Task<ShipDto> Execute(DockShipDto dockShipDto)
    {
        var validationResult = await _validator.ValidateAsync(dockShipDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        Ship shipConvert = new Ship(dockShipDto.Name, dockShipDto.GoldCargo, dockShipDto.CreatedAt, dockShipDto.Captain, dockShipDto.Status, dockShipDto.CrewSize, dockShipDto.CreatedBy);

        Ship ship = await _shipRepository.Add(shipConvert);
        return new ShipDto(ship);
    }
}