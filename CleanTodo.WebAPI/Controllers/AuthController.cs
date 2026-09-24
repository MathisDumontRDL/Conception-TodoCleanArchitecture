using CleanTodo.Application.UseCase;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.DTOS.Auth;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController(GetAllUsersUseCase _getAllUsersUseCase, LoginUseCase loginUseCase, RegisterUseCase _registerUseCase, IUserRepository _userRepository, JwtService _jwtService) : ControllerBase
{
    /* public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
     {
         var users = await getAllUsersUseCase.Execute();
         return Ok(users);
     }*/
    [HttpGet]
    [Authorize(Roles = "Admin")]
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
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            User user = await loginUseCase.Execute(loginDto);

            var token = _jwtService.GenerateToken(user.Id, user.Username, "Admin");

                return Ok(new
                {
                     token 
                });
        }
        catch (NotFoundException)
        {
            return Unauthorized();
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
