using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace GigaHurtz_Frontend.Services;
public class PositionStack
{

    static HttpClient client = new HttpClient();

    static async Task RunAsync()
    {
        // Update port # in the following line.
        client.BaseAddress = new Uri("http://api.positionstack.com/v1/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }

    static async Task<Product> GetProductAsync(string path)
    {
        Product product = ;
        HttpResponseMessage response = await client.GetAsync(path);
        if (response.IsSuccessStatusCode)
        {
            product = await response.Content.ReadAsAsync<Product>();
        }
        return product;
    }
}
    
