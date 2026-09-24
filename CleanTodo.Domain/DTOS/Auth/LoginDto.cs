using CleanTodo.Domain.Entities;

namespace CleanTodo.Domain.DTOS.Auth;

public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public LoginDto() { }


    // Devrait être fait dans Mapping -> automapper.
    public LoginDto(User user)
    {

        Username = user.Username;
        Password = user.Password;
    }
}
