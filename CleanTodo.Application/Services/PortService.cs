using CleanTodo.Domain.DTOS.Ship;
using System.Net.Http.Json;

public class PortService
{

    private readonly Broker _broker;

    public PortService(Broker broker)
    {
        _broker = broker;
    }

    public async Task<List<string>> ObtenirPortsAsync()
    {
        return await _broker.GetPortsAsync();
    }

}
