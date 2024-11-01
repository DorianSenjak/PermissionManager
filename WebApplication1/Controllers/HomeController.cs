using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Text;
using WebApplication1.Context;
using WebApplication1.Models;

public class HomeController : Controller
{
    private readonly MyDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, MyDbContext context)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()    
    {
        try
        {
            var appItems = _context.Apps
                .Select(x => new SelectListItem
                {
                    Text = x.AppName,
                    Value = x.AppID.ToString()
                }).ToList();
            ViewBag.AppDdlItems = appItems;

            var okolinaItems = _context.Okolinas
                .Select(x => new SelectListItem
                {
                    Text = x.OKLName,
                    Value = x.OKLID.ToString()
                }).ToList();
            ViewBag.OkolinaDdlItems = okolinaItems;

            return View();
        }
        catch (Exception ex)
        {
            // Handle the exception
            throw ex;
        }
    }

    [HttpPost]
    public ActionResult Index(string username, int apps, int okolina)
    {
        try
        {
            var selectedApp = _context.Apps.FirstOrDefault(x => x.AppID == apps);
            var selectedOkolina = _context.Okolinas.FirstOrDefault(x => x.OKLID == okolina);

            var appDatabaseEnvironments = _context.App_Baza_Okolinas
                .Where(x => x.IDApp == apps && x.IDOKL == okolina)
                .ToList();

            if (selectedApp == null || selectedOkolina == null || !appDatabaseEnvironments.Any())
            {
                ViewBag.Message = "Invalid selection or data not found.";
            }
            else
            {
                var messages = new StringBuilder();
                messages.AppendLine($"Molim da kreirate korisnika {username} (ako vec ne postoji) i date mu proxy prava na:");
                foreach (var appDatabaseEnvironment in appDatabaseEnvironments)
                {
                    var appDatabase = _context.App_Bazas.FirstOrDefault(x => x.IDApp == apps && x.IDBaza == appDatabaseEnvironment.IDBaza);
                    var database = _context.Bazas.FirstOrDefault(x => x.BazaID == appDatabaseEnvironment.IDBaza);

                    if (appDatabase != null && database != null)
                    {
                        messages.AppendLine($"{appDatabaseEnvironment.Link}/{database.BazaName} (applicative user: {appDatabase.Username})");
                    }
                }

                ViewBag.Message = messages.ToString();
            }

            ViewBag.AppDdlItems = _context.Apps
                .Select(x => new SelectListItem
                {
                    Text = x.AppName,
                    Value = x.AppID.ToString()
                }).ToList();

            ViewBag.OkolinaDdlItems = _context.Okolinas
                .Select(x => new SelectListItem
                {
                    Text = x.OKLName,
                    Value = x.OKLID.ToString()
                }).ToList();

            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the Index POST request.");
            throw;
        }
    }

    public IActionResult Settings()
    {
        return View();
    }

    public IActionResult Info()
    {
        try
        {
            var okolinaItems = _context.Okolinas
                .Select(x => new SelectListItem
                {
                    Text = x.OKLName,
                    Value = x.OKLID.ToString()
                }).ToList();

            ViewBag.OkolinaDdlItems = okolinaItems; 
            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading Info page.");
            throw;
        }
    }

    [HttpPost]
    public IActionResult Info(int okolina)
    {
        try
        {
            var apps = _context.App_Baza_Okolinas
                .Where(x => x.IDOKL == okolina)
                .Select(x => new
                {
                    AppName = _context.Apps
                        .Where(app => app.AppID == x.IDApp)
                        .Select(app => app.AppName)
                        .FirstOrDefault(),

                    DatabaseName = _context.Bazas
                        .Where(db => db.BazaID == x.IDBaza)
                        .Select(db => db.BazaName)
                        .FirstOrDefault(),

                    EnvironmentLink = x.Link,

                    AppUser = _context.App_Bazas
                        .Where(ab => ab.IDApp == x.IDApp && ab.IDBaza == x.IDBaza)
                        .Select(ab => ab.Username)
                        .FirstOrDefault()
                }).ToList();

            ViewBag.SelectedOkolina = okolina;
            ViewBag.Apps = apps;

            var okolinaItems = _context.Okolinas
                .Select(x => new SelectListItem
                {
                    Text = x.OKLName,
                    Value = x.OKLID.ToString()
                }).ToList();
            ViewBag.OkolinaDdlItems = okolinaItems;

            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching data for selected Okolina.");
            throw;
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
