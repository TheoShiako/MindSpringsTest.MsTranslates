using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace TranslationsMVC.Controllers;

[Authorize]
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
        try
        {
            var translators = (await translatorsData.Get()).ToList();
            return View(translators);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return View();
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(TranslatorModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await translatorsData.Insert(model);
                TempData["Success"] = "Translator added successfully";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
        }
        return View();
    }

    public async Task<IActionResult> Detail(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var translator = (await translatorsData.Get(id)).FirstOrDefault();
        if (translator == null)
        {
            return NotFound();
        }

        return View(translator);
    }

    [HttpPost]
    public async Task<IActionResult> Detail(TranslatorModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await translatorsData.Update(model);
                TempData["Success"] = "Translator updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
        }
        return View();
    }
}
