namespace CleanTodo.Domain.Entities
{
    public class Ship
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int GoldCargo { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Captain { get; set; }
        public string Status { get; set; }
        public int CrewSize { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModified { get; set; }
        public Ship(string name, int goldCargo, string captain, string status, int crewSize, string createdBy)
        {
            Id = Guid.NewGuid();
            Name = name;
            GoldCargo = goldCargo;
            CreatedAt = DateTime.Now;
            Captain = captain;
            Status = status;
            CrewSize = crewSize;
            CreatedBy = createdBy;
            LastModified = DateTime.Now;
        }
    }
}