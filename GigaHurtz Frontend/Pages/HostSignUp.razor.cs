using Microsoft.AspNetCore.Components;
using GigaHurtz_Frontend.Services;
using GigaHurtz.Common.Models;

public partial class HostSignUp
{
    private string name;
    private string email;
    private string password;
    private string phoneNumber;
    private string address;
    private int MaxTenants;
    private string[] languages;
    private bool hostKids;
    private bool providesFood;
    private bool malePref;
    private bool femalePref;
    private string genderPref;

    private int availableRooms;
    private string imageUrl;
    private readonly IApiService apiService;
    private readonly NavigationManager NavigationManager;


    public HostSignUp(IApiService apiService, NavigationManager navigationManager) 
    {
        this.apiService = apiService;
        NavigationManager = navigationManager;
    }

    public void HostPage()
    {
        NavigationManager.NavigateTo("hpage");
    }

    private String convertToString(bool malePref, bool femalePref)
    {
        if (malePref && femalePref)
            return "Mixed";
        else if (malePref)
            return "Male";
        else if (femalePref)
            return "Female";
        else
            return "None";
    }

    public async Task SubmitHostInfo()
    {
        var hostId = await apiService.Register(email, password);
        var hostObject = new HostModel(
            Id: hostId,
            Name: name,
            Email: email,
            Phone: phoneNumber,
            Address: address,
            MaxTenants: MaxTenants,
            Languages: languages,
            Kids: hasKids,
            Cooks: hasFood,
            GenderPref: convertToString(malePref, femalePref),
            AvailableRooms: availableRooms,
            ImageUrl: imageUrl
        );
        await apiService.AddHost(hostObject);
        NavigationManager.NavigateTo("login");
    }
}



