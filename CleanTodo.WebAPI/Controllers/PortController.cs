using CleanTodo.Application.UseCase;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.DTOS.Auth;
using CleanTodo.Domain.DTOS.Ship;
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
public class PortController(GetAllPortsUseCase _getAllPortsUseCase) : ControllerBase
{
    [HttpGet("users")]
    public async Task<ActionResult<IEnumerable<string>>> GetAll()
    {
        var ports = await _getAllPortsUseCase.Execute();

        return Ok(ports);
    }



}
