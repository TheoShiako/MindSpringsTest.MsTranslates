using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Add services and configuration using the 'ServiceConfiguration' Class
builder.ConfigureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Execute Db initialiser
using var scope = app.Services.CreateScope();
var initializer = scope.ServiceProvider.GetRequiredService<InitializeIdentityDb>();

await initializer.RunAsync();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Add blazor usage to MVC
app.MapBlazorHub();

//Add Serilog usage
app.UseSerilogRequestLogging();

app.Run();
