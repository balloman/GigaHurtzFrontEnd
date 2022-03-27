using GigaHurtz.Common.Models;

namespace GigaHurtz_Frontend.Pages;

public partial class HostPage
{
    private HostModel host = HostModel.Empty;

    /// <inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
        Refresh();
    }

    private async Task Refresh()
    {
        var hostId = ApiService.UserId;
        if (hostId is not null) host = await ApiService.GetHost(hostId);
    }
}