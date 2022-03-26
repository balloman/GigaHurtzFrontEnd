namespace GigaHurtzApi.Models;

public record Refugee(string Id, string Name, int HouseholdSize, string Gender, string Location, bool HasKids, 
                      bool ActivelyLooking, string Phone, string Email);
