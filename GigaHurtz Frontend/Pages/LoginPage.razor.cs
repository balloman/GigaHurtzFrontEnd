using GigaHurtz_Frontend.Services;

namespace GigaHurtz_Frontend.Pages;

public partial class LoginPage
{
    private string Username { get; set; } = string.Empty;
    private string Password { get; set; } = string.Empty;

    private async Task Login()
    {
        var uid = await ApiService.Login(Username, Password);
        var role = await ApiService.GetRole(uid);
        switch (role)
        {
            case IApiService.Role.Host:
                NavigationManager.NavigateTo("/hpage");
                break;
            case IApiService.Role.Refugee:
                NavigationManager.NavigateTo("/rpage");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
