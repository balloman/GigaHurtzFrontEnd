using GigaHurtz.Common.Models;
using Microsoft.JSInterop;

namespace GigaHurtz_Frontend.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _client;
    private readonly IJSRuntime _runtime;
    private const string BASE_ADDRESS = "https://gigahurtz-api.herokuapp.com";

    public string? UserId { get; set; }

    public ApiService(IJSRuntime jsRuntime)
    {
        _runtime = jsRuntime;
        _client = new HttpClient
        {
            BaseAddress = new Uri(BASE_ADDRESS)
        };
    }

    public IEnumerable<string> Languages
    {
        get
        {
            using var tempClient = new HttpClient();

        }
    }

    public async Task<HostModel> GetHost(string userId)
    {
        var host = await _client.GetFromJsonAsync<HostModel>($"Host/{userId}");
        if (host is null) throw new HttpRequestException("Could not find the user with that id...");
        return host;
    }

    public async Task<Refugee> GetRefugee(string userId)
    {
        var refugee = await _client.GetFromJsonAsync<Refugee>($"Refugee/{userId}");
        if (refugee is null) throw new HttpRequestException("Could not find the user with that id...");
        return refugee;
    }

    public async Task<IApiService.Role> GetRole(string userId)
    {
        var result = int.Parse(await _client.GetStringAsync($"user/{userId}"));
        return result switch
        {
            0 => IApiService.Role.Host,
            1 => IApiService.Role.Refugee,
            _ => throw new InvalidOperationException("The result shouldn't have been this what the")
        };
    }

    public async Task<string?> Login(string email, string password)
    {
        var uid = await _runtime.InvokeAsync<string>("FirebaseFunctions.login", new object?[] {email, password});
        return uid;
    }

    public async Task<string> Register(string email, string password)
    {
        var uid = await _runtime.InvokeAsync<string>("FirebaseFunctions.login", new object?[] { email, password });
        return uid;
    }

    public async Task AddHost(HostModel host)
    {
        var response = await _client.PostAsJsonAsync($"host", host);
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException("There was an error posting to the database");
    }

    public async Task AddRefugee(Refugee refugee)
    {
        var response = await _client.PostAsJsonAsync($"refugee", refugee);
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException("There was an error posting to the database");
    }
}