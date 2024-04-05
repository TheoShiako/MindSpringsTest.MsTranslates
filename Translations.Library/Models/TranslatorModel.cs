using System.ComponentModel.DataAnnotations;

namespace Translations.Library.Models;
public class TranslatorModel
{
    public int Id { get; set; }

    [Required(ErrorMessage ="Kindly enter a name")]
    public string Name { get; set; } = "";

    [Display(Name="Base Url")]
    [Required(ErrorMessage ="Kindly enter the base url for the Translator API")]
    public string BaseUrl { get; set; } = "";

    public string? ApiKey { get; set; }
}
