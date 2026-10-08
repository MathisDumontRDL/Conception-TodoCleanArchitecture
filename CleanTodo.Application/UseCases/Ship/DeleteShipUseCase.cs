using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class DeleteShipUseCase
{
    private readonly IShipRepository _shipRepository;

    public DeleteShipUseCase(IShipRepository shipRepository)
    {
        _shipRepository = shipRepository;
    }

    public async Task Execute(Guid id)
    {
        Ship? ship = await _shipRepository.FindById(id);

        if (ship == null)
        {
            throw new NotFoundException(id);
        }

        await _shipRepository.Delete(id);
    }
}