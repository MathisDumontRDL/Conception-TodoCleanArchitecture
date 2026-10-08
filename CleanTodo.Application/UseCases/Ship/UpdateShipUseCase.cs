using CleanTodo.Domain.DTOS.Ship;
using CleanTodo.Domain.DTOS.Todo;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;
using FluentValidation;

namespace CleanTodo.Application.UseCase;

public class UpdateShipUseCase
{
    private readonly IShipRepository _shipRepository;
    private readonly IValidator<ModifyShipDto> _validator;

    public UpdateShipUseCase(IShipRepository shipRepository, IValidator<ModifyShipDto> validator)
    {
        _shipRepository = shipRepository;
        _validator = validator;
    }

    public async Task<ShipDto> Execute(Guid id, ModifyShipDto modifyShip)
    {
        var validationResult = await _validator.ValidateAsync(modifyShip);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var ship = await _shipRepository.FindById(id);

        if (ship == null)
        {
            throw new NotFoundException(id);
        }

        ship.Name = modifyShip.Name;
        ship.GoldCargo = modifyShip.GoldCargo;
        ship.Captain = modifyShip.Captain;
        ship.CrewSize = modifyShip.CrewSize;
        ship.LastModified = DateTime.Now;

        await _shipRepository.Update(ship);

        return new ShipDto
        {
            Id = ship.Id,
            Name = ship.Name,
            GoldCargo = ship.GoldCargo,
            CreatedAt = ship.CreatedAt,
            Captain = ship.Captain,
            Status = ship.Status,
            CrewSize = ship.CrewSize,
            CreatedBy = ship.CreatedBy,
            LastModified = ship.LastModified
        };
    }
}