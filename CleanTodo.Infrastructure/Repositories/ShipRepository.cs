using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class ShipRepository : IShipRepository
{
    private readonly AppDbContext _context;

    public ShipRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Ship>> GetAll()
    {
        return await _context.Ships.ToListAsync();
    }
    public async Task<Ship?> FindById(Guid id)
    {
        return await _context.Ships
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();
    }


    public async Task<Ship> Add(Ship ship)
    {
        EntityEntry<Ship> newShip = await _context.Ships.AddAsync(ship);
        await _context.SaveChangesAsync();
        return newShip.Entity; 
    }

    public async Task Delete(Guid id)
    {
        var ship = await _context.Ships.FindAsync(id);
        if (ship != null)
        {
            _context.Ships.Remove(ship);
            await _context.SaveChangesAsync();
        }
    }

}
