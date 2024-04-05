namespace Translations.Library.Dtos;
public class SearchErrorDto
{
    public ErrorResponse Error { get; set; } = new();

    public class ErrorResponse
    {
        public int Code { get; set; }
        public string Message { get; set; } = "";

    }
}
