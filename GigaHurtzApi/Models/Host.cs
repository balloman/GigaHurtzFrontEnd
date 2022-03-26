using System.Collections.Immutable;

namespace GigaHurtzApi.Models;

public record Host(string Id, string Name, int AvailableRooms, int MaxTenants, string Address, 
                   IEnumerable<string> Languages, bool Kids, bool Cooks, IEnumerable<string> GenderPref, string Phone, 
                   string Email);
