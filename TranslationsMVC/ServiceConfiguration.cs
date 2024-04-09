using Serilog;

namespace TranslationsMVC;

public static class ServiceConfiguration
{
    // Add services to the container.
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews();

        //Configure database for entity framework to be used for identity system
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddIdentity<AppUser, IdentityRole>(
            options =>
            {
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

        //Add identity database initializer
        builder.Services.AddScoped<InitializeIdentityDb>();

        //Add custom services
        builder.Services.AddSingleton<ISqlServerConnection, SqlServerConnection>();
        builder.Services.AddSingleton<ITranslatorsData, TranslatorData>();
        builder.Services.AddSingleton<ITranslationData, TranslationData>();
        builder.Services.AddScoped<ITranslateText, TranslateText>();
        builder.Services.AddHttpClient();

        //Add blazor to the container
        builder.Services.AddServerSideBlazor();

        // Add serilog services to the container and read config from appsettings
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));
        Log.Logger = new LoggerConfiguration()
               .CreateLogger();
    }
}
