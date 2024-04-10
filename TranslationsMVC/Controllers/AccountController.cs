namespace TranslationsMVC.Controllers;

//Controller handles User Authentication
public class AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) : Controller
{
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            //login
            var loginResult = await signInManager.PasswordSignInAsync(model.Username!, model.Password!, model.RememberMe, false);

            if (loginResult.Succeeded)
            {
                TempData["Success"] = "Login successful.";
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", "Invalid login attempt");
        }
        return View(model);
    }

    [Authorize]
    public IActionResult Register(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            AppUser user = new()
            {
                Name = model.Name,
                UserName = model.UserName,
                Email = model.Email,
            };

            var createUserResult = await userManager.CreateAsync(user, model.Password!);

            if (createUserResult.Succeeded)
            {
                TempData["Success"] = "User created successfully.";
                return View();
            }
            foreach (var error in createUserResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }

    [Authorize]
    public IActionResult ChangePassword(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordVM model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await userManager.ChangePasswordAsync(user, model.OldPassword!, model.NewPassword!);
            if (changePasswordResult.Succeeded)
            {
                TempData["Success"] = "Your password has been changed.";
                await signInManager.RefreshSignInAsync(user);
                return View();
            }
            foreach (var error in changePasswordResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    private IActionResult RedirectToLocal(string? returnUrl)
    {
        return !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)
            ? Redirect(returnUrl)
            : RedirectToAction(nameof(HomeController.Index), "Home");
    }
}
