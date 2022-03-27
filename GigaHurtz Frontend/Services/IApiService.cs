using GigaHurtz.Common.Models;

namespace GigaHurtz_Frontend.Services;

public interface IApiService
{
    public string? UserId { get; set; }

    public IEnumerable<string> Languages { get; }

    public Task<HostModel> GetHost(string userId);

    public Task<Refugee> GetRefugee(string userId);

    public Task<Role> GetRole(string userId);

    ///<summary>Logs a user in and returns their user id</summary>
    ///<returns>The user id if they exist, null otherwise</returns>
    public Task<string?> Login(string email, string password);

    ///<summary>Same as Login, but if they don't exist</summary>
    public Task<string> Register(string email, string password);

    public Task AddHost(HostModel host);

    public Task AddRefugee(Refugee refugee);

    public Task AddRequest(HostModel host, Refugee refugee);

    public Task<IEnumerable<Refugee>> GetRequests(HostModel host);

    public Task<IEnumerable<HostModel>> GetHosts();

    public Task<IEnumerable<Compatibility>> GetCompatibility(Refugee refugee); 

    public enum Role
    {
        Host,
        Refugee
    }

}