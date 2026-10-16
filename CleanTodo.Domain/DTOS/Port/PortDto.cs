namespace CleanTodo.Domain.DTOS.Ship;
using CleanTodo.Domain.Entities;


public class PortDto
{
    public string User { get; set; }

    public PortDto(string user)
    {

        User = user;
    }
}
