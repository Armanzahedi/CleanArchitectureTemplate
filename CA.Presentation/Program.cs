using CA.Application;
using CA.Infrastructure;
using CA.Infrastructure.Identity;
using CA.Infrastructure.Persistence;
using CA.Presentation;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddPresentation()
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
app.AddIdentityEndpoints("/auth");
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