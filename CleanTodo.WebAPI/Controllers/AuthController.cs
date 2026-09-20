using CleanTodo.Application.UseCase;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.DTOS.Auth;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController(GetAllUsersUseCase _getAllUsersUseCase, LoginUseCase loginUseCase, RegisterUseCase _registerUseCase) : ControllerBase
{
    /* public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
     {
         var users = await getAllUsersUseCase.Execute();
         return Ok(users);
     }*/
    private readonly IUserRepository _userRepository;
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAll()
    {
        var users = await _getAllUsersUseCase.Execute();
        return Ok(users);
    }

    //Cadeau! pour le create. On utilise un CreatedAtAction qui retourne un code http 201 et un header location avec l'url du nouvel élément créé.
    [HttpPost("register")]
    public async Task<ActionResult<RegisterDto>> Create([FromBody] RegisterDto registerDto)
    {
        User user = await _registerUseCase.Execute(registerDto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = user.Id },
            user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<User>> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            User user = await loginUseCase.Execute(loginDto);
            return Ok(user);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("{id}")] // /api/auth/ton_id
    public async Task<ActionResult<User>> GetById(Guid id)
    {
        var user = await _userRepository.FindById(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }


}
