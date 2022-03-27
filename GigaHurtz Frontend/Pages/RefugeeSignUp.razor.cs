using GigaHurtz.Common.Models;

namespace GigaHurtz_Frontend.Pages;

public partial class RefugeeSignUp
{
    private bool ActivelyLooking => true;
    private string Address { get; set; } = string.Empty;
    private string Email { get; set; } = string.Empty;
    private int GroupSize { get; set; }
    private bool HasBoth { get; set; }
    private bool HasFemale { get; set; }
    private bool HasKids { get; set; }
    private bool HasMale { get; set; }
    private string[] Languages { get; set; } = Array.Empty<string>();

    private string Name { get; set; } = string.Empty;
    private string Password { get; set; } = string.Empty;
    private string PhoneNumber { get; set; } = string.Empty;
    private string SelectedLanguage { get; set; } = "English";


    private async Task GoToRPage()
    {
        await SubmitHostInfo();
        _NavigationManager.NavigateTo("rpage");
    }
    private void MainMenu()
    {
        _NavigationManager.NavigateTo("");
    }
    private string convertToString(bool hasMale, bool hasFemale, bool hasBoth)
    {
        if (hasMale)
            return "Male";
        if (hasFemale)
            return "Female";
        return "Both";
    }

    private async Task SubmitHostInfo()
    {
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            return;
        }
        var refugeeId = await ApiService.Register(Email, Password);
        var refugeeObject = new Refugee(
            refugeeId,
            Name,
            Email: Email,
            Phone: PhoneNumber,
            Location: Address,
            Languages: Languages,
            HasKids: HasKids,
            HouseholdSize: GroupSize,
            Gender: convertToString(HasMale, HasFemale, HasBoth),
            ActivelyLooking: ActivelyLooking
        );
        await ApiService.AddRefugee(refugeeObject);
    }
}
