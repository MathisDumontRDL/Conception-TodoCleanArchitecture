using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.DTOS.Auth;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class LoginUseCase
{
    private readonly IUserRepository _userRepository;

    public LoginUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<LoginDto> Execute(Guid id)
    {
        User? user = await _userRepository.FindById(id);
        if (user == null)
        {
            throw new NotFoundException(id);
        }
        return new LoginDto(user);
    }

    public async Task<User> Execute(LoginDto loginDto)
    {
        User? user = await _userRepository.FindByUsername(loginDto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
        {
            throw new NotFoundException(loginDto.Username);
        }
        return user;
    }
}