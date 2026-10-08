using CleanTodo.Domain.Entities;

namespace CleanTodo.Domain.Interfaces.Repositories;

public interface IShipRepository
{
    Task<List<Ship>> GetAll();
    Task<Ship?> FindById(Guid id);
    Task<Ship> Add(Ship ship);
    Task<Ship> Update(Ship ship);
    Task Delete(Guid id);

}
