using test_task.Components;
using test_task.Services;
using test_task.Interfaces;
using test_task.DataBase;
using Microsoft.EntityFrameworkCore;
using test_task.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<IHelpdeskRequestService, HelpdeskRequestService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("HelpdeskRequestsDb"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


// Minimal API 
app.MapPost("/api/requests", async (HelpdeskRequest request, AppDbContext db) =>
{
    db.HelpdeskRequests.Add(request);
    await db.SaveChangesAsync();
    return Results.Created($"/api/requests/{request.Id}", request);
});

app.MapGet("/api/requests/active", async (AppDbContext db) =>
{
    var activeRequests = await db.HelpdeskRequests
                                  .Where(request => !request.IsResolved)
                                  .OrderByDescending(request => request.Deadline)
                                  .ToListAsync();
    return Results.Ok(activeRequests);
});

app.MapPost("/api/requests/{id}/resolve", async (int id, AppDbContext db) =>
{
    var request = await db.HelpdeskRequests.FindAsync(id);
    if (request == null) return Results.NotFound();

    request.IsResolved = true;
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();
