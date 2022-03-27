using GigaHurtz.Common.Models;

namespace GigaHurtz_Frontend.Pages;
public partial class RefugeeSignUp
{

    private String name;
    private String email;
    private String password;
    private String phoneNumber;
    private String address;
    private int groupSize;
    private String[] languages;
    private bool hasKids;
    private bool hasEmail;
    private bool hasMale;
    private bool hasFemale;
    private bool hasBoth;
    private bool activelyLooking;
    

    private async Task GoToRPage()
    {
        await SubmitHostInfo();
        _NavigationManager.NavigateTo("rpage");
    }

    private String convertToString(bool hasMale, bool hasFemale, bool hasBoth)
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
            Id: refugeeId,
            Name: name,
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