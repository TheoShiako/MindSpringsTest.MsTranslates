using Microsoft.Extensions.Logging;

namespace Translations.Library.DataAccess;

public class TranslateText : ITranslateText
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ITranslatorsData translatorsData;
    private readonly ITranslationData translationData;
    private readonly ILogger logger;

    public TranslateText(IHttpClientFactory httpClientFactory,
                         ITranslatorsData translatorsData,
                         ITranslationData translationData,
                         ILogger<TranslateText> logger)
    {
        this.httpClientFactory = httpClientFactory;
        this.translatorsData = translatorsData;
        this.translationData = translationData;
        this.logger = logger;
    }

    //Method to translate search text
    public async Task<TranslationModel> Translate(TranslationModel model)
    {
        var translator = await GetTranslator(model.Translator);

        var translation = new TranslationModel()
        {
            SearchText = model.SearchText,
            Translator = translator
        };


        using var client = httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(translator.BaseUrl);
        var response = await client.GetAsync($@"?text=" + translation.SearchText);
        if (response.IsSuccessStatusCode)
        {
            SearchResponseDto? searchResponse = await response.Content.ReadFromJsonAsync<SearchResponseDto>();
            var translatedText = searchResponse?.Contents.Translated!;

            translation.TranslatedText = translatedText;
            translation.Status = 1;
        }
        else
        {
            var error = await response.Content.ReadFromJsonAsync<SearchErrorDto>();

            var errorMessage = error?.Error.Message;

            translation.Error = errorMessage;
        }

        _ = SaveTranslation(translation);
        return translation;
    }

    /// <summary>
    /// Get the translator to be used for translation from database
    /// </summary>
    /// <param name="translator"></param>
    /// <returns></returns>
    private async Task<TranslatorModel> GetTranslator(TranslatorModel translator)
        => (await translatorsData.Get(translator.Id))?.First()!;

    /// <summary>
    /// Save the 
    /// </summary>
    /// <param name="translation"></param>
    /// <returns></returns>
    private async Task SaveTranslation(TranslationModel translation)
    {
        try
        {
            await translationData.Insert(translation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error saving translation info to database");
        }
    }
}
