using GigaHurtz.Common.Models;
using GigaHurtz_Frontend.Services;

namespace GigaHurtz_Frontend.Pages;

public partial class RefugeePage
{
    private Refugee refugee = Refugee.Empty;
    private List<CompatModel> compatibilities = new List<CompatModel>();

    public RefugeePage()
    {
    }

    public async Task Refresh()
    {
        refugee = await ApiService.GetRefugee(ApiService.UserId);
        foreach (var compatibility in await ApiService.GetCompatibility(refugee))
        {
            compatibilities.Add(new CompatModel
            {
                Host = await ApiService.GetHost(compatibility.HostId),
                CompatScore = compatibility.CompatibilityScore,
                Name = compatibility.Name
            });
        }
    }

    /// <inheritdoc />
    protected override async Task OnInitializedAsync()
    {
        await Refresh();
    }

    private async Task ButtonClick(HostModel host)
    {
        await ApiService.AddRequest(host, refugee);
    }

    public class CompatModel
    {
        public string Name { get; set; }
        public HostModel Host { get; set; }
        public double CompatScore { get; set; }
    }
}
