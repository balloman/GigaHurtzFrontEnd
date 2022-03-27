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

    static async Task RunAsync()
    {
        // Update port # in the following line.
        client.BaseAddress = new Uri("http://api.positionstack.com/v1/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }

    static async Task<string> GetLocationData()
    {
        var address = "";
        var query = $"forward?access_key={API_ACCESS_KEY}&query={address}";
        var data = await client.GetStringAsync(query);
        return data;
    }
}