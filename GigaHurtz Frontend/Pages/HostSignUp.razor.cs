using GigaHurtz.Common.Models;

namespace GigaHurtz_Frontend.Pages;
public partial class HostSignUp
{
    private string Name { get; set; } = string.Empty;
    private string Email { get; set; } = string.Empty;
    private string Password { get; set; } = string.Empty;
    private string PhoneNumber { get; set; } = string.Empty;
    private string Address { get; set; } = string.Empty;
    private int MaxTenants { get; set; }
    private string[] Languages { get; } = Array.Empty<string>();
    private bool HostKids { get; set; }
    private bool ProvidesFood { get; set; }
    private bool MalePref { get; set; }
    private bool FemalePref { get; set; }

    private int AvailableRooms { get; set; }
    private string SelectedLanguage { get; set; } = "English";

    private string ImageUrl { get; } =
        "https://image.shutterstock.com/image-vector/no-house-icon-illustration-isolated-260nw-637489690.jpg";

    private async Task HostPage()
    {
        await SubmitHostInfo();
        NavigationManager.NavigateTo("hpage");
    }

    private string[] ConvertToString(bool malePref, bool femalePref)
    {
        if (malePref && femalePref)
            return new[] {"Female", "Male"};
        else if (malePref)
            return new[] { "Male" };
        else if (femalePref)
            return new[] {"Female"};
        return new[] {"Female", "Male"};
    }

    private async Task SubmitHostInfo()
    {
        var hostId = await ApiService.Register(Email, Password);
        var hostObject = new HostModel(
            Id: hostId,
            Name,
            Email: Email,
            Phone: PhoneNumber,
            Address: Address,
            MaxTenants: MaxTenants,
            Languages: Languages,
            Kids: HostKids,
            Cooks: ProvidesFood,
            GenderPref: ConvertToString(MalePref, FemalePref),
            AvailableRooms: AvailableRooms,
            ImageUrl: ImageUrl,
            Requests: Array.Empty<string>()
        );
        await ApiService.AddHost(hostObject);
        NavigationManager.NavigateTo("/login");
    }
}



