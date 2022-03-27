using Microsoft.AspNetCore.Components;
using GigaHurtz_Frontend.Services;
using GigaHurtz.Common.Models;

namespace GigaHurtz_Frontend.Pages;
public partial class HostSignUp
{
    private string name;
    private string email;
    private string password;
    private string phoneNumber;
    private string address;
    private int maxTenants;
    private string[] languages;
    private bool hostKids;
    private bool providesFood;
    private bool malePref;
    private bool femalePref;
    private string genderPref;

    private int availableRooms;
    private string imageUrl;

    public async Task HostPage()
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

    public async Task SubmitHostInfo()
    {
        var hostId = await ApiService.Register(email, password);
        var hostObject = new HostModel(
            Id: hostId,
            Name: name,
            Email: email,
            Phone: phoneNumber,
            Address: address,
            MaxTenants: maxTenants,
            Languages: languages,
            Kids: hostKids,
            Cooks: providesFood,
            GenderPref: ConvertToString(malePref, femalePref),
            AvailableRooms: availableRooms,
            ImageUrl: imageUrl
        );
        await ApiService.AddHost(hostObject);
        NavigationManager.NavigateTo("login");
    }
}



