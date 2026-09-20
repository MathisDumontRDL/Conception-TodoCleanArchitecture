using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class GetAllUsersUseCase
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    //public async Task<IList<UserDto>> Execute()
    public async Task<IList<User>> Execute()
    {
        /*var users = await _userRepository.GetAll();
        return users.Select(x => new UserDto(x)).ToList();*/
        return await _userRepository.GetAll();
    }
}