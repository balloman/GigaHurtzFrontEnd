using System.Net.Mime;
using GigaHurtz_Common.Models;
using Host = GigaHurtz_Common.Models.Host;

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
