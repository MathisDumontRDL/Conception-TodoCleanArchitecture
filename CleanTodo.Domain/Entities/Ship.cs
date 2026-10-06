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
        public Ship(string name, int goldCargo, DateTime createdAt, string captain, string status, int crewSize, string CreatedBy, DateTime lastModified)
        {
            Id = Guid.NewGuid();
            Name = name;
            GoldCargo = goldCargo;
            CreatedAt = createdAt;
            Captain = captain;
            Status = status;
            CrewSize = crewSize;
            CreatedAt = createdAt;
            LastModified = lastModified;
        }
    }
}