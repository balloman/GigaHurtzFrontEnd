using GigaHurtz.Common.Models;

namespace GigaHurtz_Frontend.Pages;

public partial class RefugeeSignUp
{
    private bool activelyLooking;
    private string address;
    private string email;
    private int groupSize;
    private bool hasBoth;
    private bool hasEmail;
    private bool hasFemale;
    private bool hasKids;
    private bool hasMale;
    private string[] languages;

    private string name;
    private string password;
    private string phoneNumber;


    private async Task GoToRPage()
    {
        await SubmitHostInfo();
        _NavigationManager.NavigateTo("rpage");
    }

    private string convertToString(bool hasMale, bool hasFemale, bool hasBoth)
    {
        if (hasMale)
            return "Male";
        if (hasFemale)
            return "Female";
        return "Both";
    }

    public async Task SubmitHostInfo()
    {
        var refugeeId = await ApiService.Register(email, password);
        var refugeeObject = new Refugee(
            refugeeId,
            name,
            Email: email,
            Phone: phoneNumber,
            Location: address,
            Languages: languages,
            HasKids: hasKids,
            HouseholdSize: groupSize,
            Gender: convertToString(hasMale, hasFemale, hasBoth),
            ActivelyLooking: activelyLooking
        );
        await ApiService.AddRefugee(refugeeObject);
    }
}
