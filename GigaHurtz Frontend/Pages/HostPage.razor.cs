using GigaHurtz.Common.Models;

namespace GigaHurtz_Frontend.Pages;

public partial class HostPage
{
    private HostModel host = HostModel.Empty;
    private List<Refugee> requests = new List<Refugee>();

    /// <inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
        await Refresh();
    }

    private async Task Refresh()
    {
        var hostId = ApiService.UserId;
        if (hostId is not null) host = await ApiService.GetHost(hostId);
        var requestIds = host.Requests;
        foreach (var id in requestIds)
        {
            requests.Add(await ApiService.GetRefugee(id));
        }
        Console.WriteLine(host);
    }
}