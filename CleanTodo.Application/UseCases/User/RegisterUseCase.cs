using CleanTodo.Application.Validators;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.DTOS.Auth;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;
using FluentValidation;

namespace CleanTodo.Application.UseCase;

public class RegisterUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<RegisterDto> _validator;

    public RegisterUseCase(IUserRepository userRepository, IValidator<RegisterDto> validator)
    {
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<User> Execute(RegisterDto registerDto)
    {
        var validationResult = await _validator.ValidateAsync(registerDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

        User user = new User(registerDto.Username, hashedPassword);

        return await _userRepository.Add(user);

    }
}