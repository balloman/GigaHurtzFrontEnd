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
        
        // var refugee = await _dbService.GetRefugee(uid);
        // var host = await _dbService.GetHost(uid);
        
        // var score = 0.0;
        
        // score += ( 25 - (5 *  (host.MaxTenants - refugee.HouseholdSize)) );     /// # of people

        // score += ( 50 - (2 * (host.Address - refugee.Location)) ); /// Distance
        
        // if (host.GenderPref.contains(refugee.Gender))      /// Gender
        //     score += 5;
        // else
        //     score += 10;

        // if (host.Kids)      /// Kids
        //     score += 8;
        // else
        //     score += 4;

        // if (host.Languages.Contains(refugee.Languages))     /// Language
        //     score += 5;
        
        // if (host.Cooks)     // Can cook
        //     score += 4;

        // score += ( 10 / refugee.HouseholdSize * host.AvailableRooms );      /// # of rooms

        // score = (score / 112) * 100;        // Final compatability score        

        // return null;

        throw new NotImplementedException();
    }
}
