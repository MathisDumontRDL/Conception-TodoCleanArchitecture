using CleanTodo.Domain.DTOS.Ship;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Interfaces.Repositories;
using FluentValidation;
namespace CleanTodo.Application.UseCase;

public class CreateShipUseCase
{
    private readonly IShipRepository _shipRepository;
    private readonly IValidator<CreateShipDto> _validator;


    public CreateShipUseCase(IShipRepository shipRepository, IValidator<CreateShipDto> validator)
    {
        _shipRepository = shipRepository;
        _validator = validator;
    }

    public async Task<ShipDto> Execute(CreateShipDto createdShip)
    {
        var validationResult = await _validator.ValidateAsync(createdShip);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        Ship shipConvert = new Ship(createdShip.Name, createdShip.GoldCargo, createdShip.Captain, createdShip.Status, createdShip.CrewSize, createdShip.CreatedBy);

        Ship ship = await _shipRepository.Add(shipConvert);
        return new ShipDto(ship);
    }
}