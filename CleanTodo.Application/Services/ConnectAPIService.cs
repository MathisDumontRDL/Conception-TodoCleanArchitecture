using CleanTodo.Domain.DTOS.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

public class ConnectAPIService
{

    private readonly IConfiguration _config;
    public ConnectAPIService(IConfiguration config)
    {
        _config = config;
    }
    public string Autorisation(Guid userId)
    {
        using HttpClient client = new HttpClient();

        var appURL = _config.GetSection("ApiURL");
        var appInfos = _config.GetSection("AppInfos");
        var url = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appURL["URL"]));
        var id = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appInfos["ID"]));
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appInfos["Secret"]));
        client.DefaultRequestHeaders.Add("x-client-id", appInfos["ID"]);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", appInfos["Secret"]);
        return userId;
    }
}