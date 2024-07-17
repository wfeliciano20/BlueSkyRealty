using AspNETcore.BSR.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages(options =>
{
    //new code
    options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
});
builder.Services.AddSingleton<HomeService>();

var app = builder.Build();

app.UseStaticFiles();

app.MapRazorPages();

app.Run();
