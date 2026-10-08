using CleanTodo.Domain.Entities;

namespace CleanTodo.Domain.Interfaces.Repositories;

public interface IShipRepository
{
    Task<List<Ship>> GetAll();
    Task<Ship?> FindById(Guid id);
    Task<Ship> Add(Ship ship);
    //Task<Todo> Update(Todo todo);
    //Task Delete(Guid id);

}
