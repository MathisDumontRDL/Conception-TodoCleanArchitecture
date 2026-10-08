namespace CleanTodo.Domain.DTOS.Ship;
using CleanTodo.Domain.Entities;


public class ModifyShipDto
{
    public string Name { get; set; }
    public int GoldCargo { get; set; }
    public string Captain { get; set; }
    public int CrewSize { get; set; }

    public ModifyShipDto() { }

    public ModifyShipDto(Ship ship)
    {
        Name = ship.Name;
        GoldCargo = ship.GoldCargo;
        Captain = ship.Captain;
        CrewSize = ship.CrewSize;
    }
}