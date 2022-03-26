using System.Net.Mime;
using GigaHurtz.Common.Models;
using Host = GigaHurtz.Common.Models.Host;

namespace GigaHurtzApi.Services;

public interface IDbService
{
    /// <summary>
    /// Attempts to create a host
    /// </summary>
    /// <exception cref="DbException">Thrown if there is some issue adding the host</exception>
    public Task AddHost(Host host);

    /// <summary>
    /// Attempts to create a refugee
    /// </summary>
    /// <exception cref="DbException">Thrown if there is some issue adding the refugee</exception>
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

    /// <summary>Gets the role for a specifc user</summary>
    /// <returns>Either a 0 for a host, 1 for a refugee, or null if the user is not found</returns>
    public Task<int?> GetRole(string id);

    /// <summary>
    /// Attempts to upload a file to storage
    /// </summary>
    /// <returns>The url of the uploaded file</returns>
    public Task<string> UploadFile(string path, Stream fileStream, ContentType fileType);

    public class DbException : Exception
    {
        public DbException(string message) : base(message)
        {
        }
    }
}
