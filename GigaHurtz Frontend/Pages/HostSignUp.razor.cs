/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

public partial class HostSignUp
{
    private string name;
    private string email;
    private string password;
    private string phoneNumber;
    private string address;
    private int MaxTenants;
    private string[] languages;
    private bool hasKids;
    private bool hasFood;
    private bool[] genderPref;


    public void HostPage()
    {
        NavigationManager.NavigateTo("hpage");
    }

    // public async Task SubmitHostInfo()
    public void SubmitHostInfo()
    {
        // Pull User data for the input email and password
        var uid = await JSRuntime.InvokeAsync<string>("FirebaseFunctions.login", new[] { Email, Password });

        // Check if the email and password are valid
        if (string.IsNullOrEmpty(uid))
        {
            Console.WriteLine("Invalid Login...");
            error = "Invalid Login...";
            return;
        }

        // Add the User data into the database
        await Handler.CreateUser(uid, new User(Email, Name, new List<string>()));
        NavigationManager.NavigateTo("login");
    }
}
*/


