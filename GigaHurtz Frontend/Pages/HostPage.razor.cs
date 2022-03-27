using GigaHurtz.Common.Models;
using GigaHurtz_Frontend.Services;

namespace GigaHurtz_Frontend.Pages;

public partial class HostPage {

    private HostModel host;

    public HostPage(IApiService api)
    {
        var hostId = api.UserId;
        if (hostId is not null) host = api.GetHost(hostId).Result;
    }

}