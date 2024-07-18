using AspNETcore.BSR.Services;
using AspNETcore.BSR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HomeContext>(opt => opt.UseSqlite("Data Source=bsr.db"));

builder.Services.AddRazorPages(options =>
{
    //new code
    options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
});
builder.Services.AddScoped<HomeService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HomeContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}

app.UseStaticFiles();

app.MapRazorPages();

app.Run();
