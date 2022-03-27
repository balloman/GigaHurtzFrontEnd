using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using GigaHurtz_Frontend.Data;
using GigaHurtz_Frontend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBlazorise(options =>
{
    options.Immediate = true;
}).AddBootstrapProviders().AddFontAwesomeIcons();
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
