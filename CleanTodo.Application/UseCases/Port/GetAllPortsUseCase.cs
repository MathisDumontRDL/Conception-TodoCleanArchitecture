using CleanTodo.Domain.DTOS.Ship;
using CleanTodo.Domain.DTOS.Todo;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class GetAllPortsUseCase
{
    private readonly PortService _portService;

    public GetAllPortsUseCase(PortService portService)
    {
        _portService = portService;
    }

    public async Task<IList<string>> Execute()
    {
        return await _portService.ObtenirPortsAsync();
    }
}