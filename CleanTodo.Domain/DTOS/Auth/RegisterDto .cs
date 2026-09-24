using CleanTodo.Domain.Entities;

namespace CleanTodo.Domain.DTOS.Auth;

public class RegisterDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public RegisterDto() { }


    // Devrait être fait dans Mapping -> automapper.
    public RegisterDto(User user, string confirmPassword)
    {

        Username = user.Username;
        Password = user.Password;
    }
}
