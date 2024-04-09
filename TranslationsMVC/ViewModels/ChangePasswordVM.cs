namespace TranslationsMVC.ViewModels;

public class ChangePasswordVM
{
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Current password")]
    public string? OldPassword { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "New password")]
    public string? NewPassword { get; set; }

    [Compare("NewPassword", ErrorMessage = "Passwords don't match.")]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }
}
