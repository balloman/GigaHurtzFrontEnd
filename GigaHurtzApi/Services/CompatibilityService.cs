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
    /// 
    /// Assumes host meets minimum requirements
    public async Task<IImmutableList<Compatibility>> GetCompatForUser(string uid)
    {
        
        var refugee = await _dbService.GetRefugee(uid);
        if (refugee is null) throw new ArgumentException("No refugee found with that uid");
        var hosts = await _dbService.GetAllHosts();
        var compat = new List<Compatibility>();
        foreach (var host in hosts)
        {
            var score = 0.0;
        
            score += ( 25 - (5 *  (host.MaxTenants - refugee.HouseholdSize)) );     /// # of people

            score += ( 50 - (2 * (0)) ); /// Distance
        
            if (host.GenderPref.Contains(refugee.Gender))      /// Gender
                score += 10;
            if (host.GenderPref.Any(s => refugee.Gender != s))
            {
                score -= 5;
            }

            if (host.Kids)      /// Kids
                score += 8;
            else
                score += 4;

            if (host.Languages.Any(s => refugee.Languages.Contains(s)))     /// Language
                score += 5;
        
            if (host.Cooks)     // Can cook
                score += 4;

            score += ( 10 / refugee.HouseholdSize * host.AvailableRooms );      /// # of rooms

            score = (score / 112) * 100;
            compat.Add(new Compatibility(host.Id, score, host.Name));
        }
        return compat.OrderByDescending(s => s.CompatibilityScore).ToImmutableList();
    }
}
