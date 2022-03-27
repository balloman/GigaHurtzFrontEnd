using System.Globalization;
using Firebase.Auth;
using GigaHurtz.Common.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace GigaHurtz_Frontend.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _client;
    private readonly FirebaseAuthProvider _auth;
    private readonly ProtectedSessionStorage _sessionStorage;
    private const string BASE_ADDRESS = "https://gigahurtz-api.herokuapp.com";
    private const string API_KEY = "AIzaSyBXamTHEreIY_sEVLmiGhoO0wARr2G4W8g";

    public ApiService(ProtectedSessionStorage sessionStorage)
    {
        _auth = new FirebaseAuthProvider(new FirebaseConfig(API_KEY));
        _sessionStorage = sessionStorage;
        _client = new HttpClient
        {
            BaseAddress = new Uri(BASE_ADDRESS)
        };
    }

    /// <inheritdoc/>
    public string? UserId { get; set; }

    public IEnumerable<string> Languages
    {
        get
        {
            var info = CultureInfo.GetCultures(CultureTypes.NeutralCultures);
            var names =  info.Select(cultureInfo =>
            {
                var nativeName = cultureInfo.NativeName;
                return nativeName;
            }).Where(s => !s.Contains("Invariant")).ToList();
            names.Remove("English");
            names.Insert(0, "English");
            return names;
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
        string? uid = null;
        try
        {
            var authResult = await _auth.SignInWithEmailAndPasswordAsync(email, password);
            uid = authResult.User.LocalId;
        } catch (FirebaseAuthException)
        {
            return uid;
        }

        UserId = uid;
        return uid;
    }

    public async Task<string> Register(string email, string password)
    {
        var auth = await _auth.CreateUserWithEmailAndPasswordAsync(email, password);
        UserId = auth.User.LocalId;
        return UserId;
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

    /// <inheritdoc/>
    public async Task AddRequest(HostModel host, Refugee refugee)
    {
        var response = await _client.PostAsync($"host/{host.Id}/{refugee.Id}", null);
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException(
                "There was an error posting to the database with code " + response.StatusCode);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Refugee>> GetRequests(HostModel host)
    {
        var list = new LinkedList<Refugee>();
        foreach (var refugeeId in host.Requests)
        {
            var refugee = await GetRefugee(refugeeId);
            list.AddLast(refugee);
        }

        return list;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<HostModel>> GetHosts()
    {
        var hosts = await _client.GetFromJsonAsync<IEnumerable<HostModel>>("host");
        if (hosts is null) throw new HttpRequestException("There was an error getting the hosts from the database");
        return hosts;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Compatibility>> GetCompatibility(Refugee refugee)
    {
        if (string.IsNullOrWhiteSpace(refugee.Id)) return Array.Empty<Compatibility>();
        var compatiblities = await _client.GetFromJsonAsync<IEnumerable<Compatibility>>($"compatibility/{refugee.Id}");
        if (compatiblities is null) throw new HttpRequestException("There was an error getting the compatibilities from the database");
        return compatiblities;
    }
}