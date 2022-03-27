using GigaHurtz.Common.Models;
using GigaHurtz_Frontend.Services;

namespace GigaHurtz_Frontend.Pages;

public partial class RefugeePage
{
    private Refugee refugee = Refugee.Empty;

    public RefugeePage()
    {
    }

    public async Task Refresh()
    {
        refugee = await ApiService.GetRefugee(ApiService.UserId);
    }

    /// <inheritdoc />
    protected override async Task OnInitializedAsync()
    {
        await Refresh();
    }
}
