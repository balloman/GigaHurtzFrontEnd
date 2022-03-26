namespace GigaHurtzApi.Models;

public record Refugee(string Id, string Name, long HouseholdSize, string Gender, string Location, bool HasKids,
                      bool ActivelyLooking, string Phone, string Email, IEnumerable<string> Languages);
