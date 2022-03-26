using System.Collections.Immutable;
using GigaHurtz.Common.Models;

namespace GigaHurtzApi.Services;

public class CompatibilityService
{
    private readonly IDbService _dbService;
    
    public CompatibilityService(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    /// <summary>
    /// Gets the compatibility for a refugee
    /// </summary>
    /// <param name="uid"></param>
    /// <returns>Immutable list of compat objects</returns>
    public IImmutableList<Compatibility> GetCompatForUser(string uid)
    {
        return null;
    }
}
