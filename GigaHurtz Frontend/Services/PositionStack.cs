using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GigaHurtz_Frontend.Services;
public class PositionStack
{
    private static string API_ACCESS_KEY = "a25e434da2ddd43ae9128b8f5d06f446";

    private static HttpClient client = new HttpClient();
    
    public PositionStack()
    {

    }
    public async void RunAsync()
    {
        // Update port # in the following line.
        client.BaseAddress = new Uri("http://api.positionstack.com/v1/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<string> GetLocationData(String address)
    {
        var query = $"forward?access_key={API_ACCESS_KEY}&output=geojson&query={address}";
        var data = await client.GetStringAsync(query);
        return data;
    }
}