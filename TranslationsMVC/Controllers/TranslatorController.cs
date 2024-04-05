namespace TranslationsMVC.Controllers;

public class TranslatorController : Controller
{
    private readonly ILogger logger;
    private readonly ITranslatorsData translatorsData;

    public TranslatorController(ILogger<TranslatorController> logger, ITranslatorsData translatorsData)
    {
        this.logger = logger;
        this.translatorsData = translatorsData;
    }

    public async Task<IActionResult> Index()
    {
        List<TranslatorModel> translators = (await translatorsData.Get()).ToList();
        return View(translators);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(TranslatorModel model)
    {
        if(ModelState.IsValid)
        {
            await translatorsData.Insert(model);
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}
