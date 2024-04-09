namespace TranslationsMVC.Data;

public class InitializeIdentityDb
{

    private readonly ApplicationDbContext context;
    private readonly UserManager<AppUser> userManager;

    //To be changed on deployment
    private const string DefaultPassword = "Password123!";

    public InitializeIdentityDb(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        this.context = context;
        this.userManager = userManager;
    }

    //Apply any pending migrations and Add admin account to database
    public async Task RunAsync()
    {
        context.Database.Migrate();

        // Create default admin user
        var adminUserName = "admin";
        var adminName = "Administrator";
        var adminEmail = "admin@localhost";
        var adminUser = new AppUser()
        {
            Name = adminName,
            UserName = adminUserName,
            Email = adminEmail,
        };

        await userManager.CreateAsync(adminUser, DefaultPassword);

        await context.SaveChangesAsync();
    }
}
