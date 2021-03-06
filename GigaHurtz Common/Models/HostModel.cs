namespace GigaHurtz.Common.Models;

public record HostModel(string Id, string Name, long AvailableRooms, long MaxTenants, string Address,
                        IEnumerable<string> Languages, bool Kids, bool Cooks, IEnumerable<string> GenderPref,
                        string Phone, string Email, string ImageUrl, IEnumerable<string> Requests)
{
    public static HostModel Empty => new("",
        "",
        0,
        0,
        "",
        Array.Empty<string>(),
        false,
        false,
        Array.Empty<string>(),
        "",
        "",
        "",
        Array.Empty<string>());
}
