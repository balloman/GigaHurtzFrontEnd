using Microsoft.AspNetCore.Components;
using GigaHurtz_Frontend.Services;
using GigaHurtz.Common.Models;

public partial class RefugeeSignUp {

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




    private void GoToRPage()
    {
        NavigationManager.NavigateTo("rpage");
    }

    private String converToString(bool hasMale, bool hasFemale, bool hasBoth)
    {
        if (hasMale)
            return "Male";
        if (hasFemale)
            return "Female";
        if (hasBoth)
            return "Both";

    }

    public async Task SubmitHostInfo()
    {
        var refugeeId = await apiService.Register(email, password);
        var refugeeObject = new Refugee(
            Id: hostId,
            Name: name,
            Email: email,
            Phone: phoneNumber,
            Location: location,
            Languages: languages,
            Kids: hasKids,
            HouseholdSize: groupSize,
            GenderPref: convertToString(hasMale, hasFemale, hasBoth),
            ActivelyLooking: activelyLooking
        );
        await apiService.AddHost(hostObject);
    }