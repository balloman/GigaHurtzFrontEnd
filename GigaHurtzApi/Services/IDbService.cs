using GigaHurtzApi.Models;
using Host = GigaHurtzApi.Models.Host;

namespace GigaHurtzApi.Services;

public interface IDbService
{
    public Task AddHost(Host host);
    public Task AddRefugee(Refugee refugee);

    /// <summary>
    /// Gets a host with the given id
    /// </summary>
    /// <returns>The host if found, otherwise null</returns>
    public Task<Host?> GetHost(string id);
    /// <summary>
    /// Gets a refugee with the given id
    /// </summary>
    /// <returns>The refugee if found, null otherwise</returns>
    public Task<Refugee?> GetRefugee(string id);
    
    public class DbException : Exception
    {
        public DbException(string message) : base(message)
        {
        }
    }
}
