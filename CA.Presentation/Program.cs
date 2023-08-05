using System.Reflection;
using CA.Application;
using CA.Application.Common.Extensions;
using CA.Infrastructure;
using CA.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);

builder.Services.AddServicesFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddServicesFromAssembly(typeof(CA.Infrastructure.AssemblyMarker).Assembly);
builder.Services.AddServicesFromAssembly(typeof(CA.Application.AssemblyMarker).Assembly);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// app.MapControllers();
app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default-area",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});
using (var scope = app.Services.CreateScope())
{
    await scope.InitializeDb();
}

app.Run();