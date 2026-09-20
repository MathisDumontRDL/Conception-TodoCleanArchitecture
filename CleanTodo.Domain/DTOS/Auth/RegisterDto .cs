using CleanTodo.Domain.Entities;

namespace CleanTodo.Domain.DTOS.Auth;

public class RegisterDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public RegisterDto() { }


    // Devrait être fait dans Mapping -> automapper.
    public RegisterDto(User user, string confirmPassword)
    {

        Id = user.Id;
        Username = user.Username;
        Password = user.Password;
        ConfirmPassword = confirmPassword;
    }
}
