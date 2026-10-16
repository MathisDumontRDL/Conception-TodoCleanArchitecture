using CleanTodo.Domain.DTOS.Ship;
using System.Net.Http.Json;
using System.Text.Json;

public class Broker
{
    private readonly HttpClient _httpClient;

    public Broker(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<string>> GetPortsAsync(CancellationToken ct = default)
    {
        var response = await _httpClient.GetAsync($"api/users", ct);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: ct);
        return json.GetProperty("users").Deserialize<List<string>>() ?? new List<string>();
    }
}

