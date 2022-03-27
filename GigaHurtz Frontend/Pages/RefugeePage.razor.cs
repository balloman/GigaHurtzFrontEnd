using GigaHurtz.Common.Models;
using GigaHurtz_Frontend.Services;

namespace GigaHurtz_Frontend.Pages;

public partial class RefugeePage {
    private Refugee? refugee;

    public RefugeePage(IApiService api)
    {
        var id = api.UserId;
        if (id is not null) refugee = api.GetRefugee(id).Result;
    }

}