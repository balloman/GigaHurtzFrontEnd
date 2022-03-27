namespace GigaHurtz.Common.Models;

public record Refugee(string Id, string Name, long HouseholdSize, string Gender, string Location, bool HasKids,
                      bool ActivelyLooking, string Phone, string Email, IEnumerable<string> Languages)
{
    public static Refugee Empty => new Refugee("", "", 0, "", "", false, false, "", "", Array.Empty<string>());
}
