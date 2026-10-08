namespace CleanTodo.Domain.DTOS.Ship;
using CleanTodo.Domain.Entities;


public class CreateShipDto
{
    public string Name { get; set; }
    public int GoldCargo { get; set; }
    public string Captain { get; set; }
    public string Status { get; set; }
    public int CrewSize { get; set; }

    public string CreatedBy { get; set; }

    public CreateShipDto() { }

    public CreateShipDto(Ship ship)
    {
        Name = ship.Name;
        GoldCargo = ship.GoldCargo;
        Captain = ship.Captain;
        Status = ship.Status;
        CrewSize = ship.CrewSize;
        CreatedBy = ship.CreatedBy;
    }
}