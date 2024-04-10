namespace Translations.Library.Models;

public class TranslationModel
{
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    [Display(Name = "Search Text")]
    public string SearchText { get; set; } = "";


    [Display(Name = "Translated Text")]
    public string? TranslatedText { get; set; }

    public TranslatorModel Translator { get; set; } = new();

    public int Status { get; set; }

    public string? Error { get; set; }

    public DateTime Timestamp { get; set; }

    public string StatusString => Status == 1 ? "Success" : "Error";
    public string Response => Status == 1 ? TranslatedText! : Error!;
}
