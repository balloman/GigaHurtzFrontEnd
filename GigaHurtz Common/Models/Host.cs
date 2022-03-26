namespace GigaHurtz_Common.Models;

public record Host(string Id, string Name, long AvailableRooms, long MaxTenants, string Address,
                   IEnumerable<string> Languages, bool Kids, bool Cooks, IEnumerable<string> GenderPref, string Phone,
                   string Email, string ImageUrl);
