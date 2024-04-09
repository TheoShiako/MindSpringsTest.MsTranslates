namespace Translations.Library.Dtos;
public class SearchResponseDto
{
    public SearchSuccess Success { get; set; } = new();

    public SearchContent Contents { get; set; } = new();


}

public class SearchSuccess
{
    public int Total { get; set; }
}

public class SearchContent
{
    public string Translated { get; set; } = "";
    public string Text { get; set; } = "";
    public string Translation { get; set; } = "";
}
