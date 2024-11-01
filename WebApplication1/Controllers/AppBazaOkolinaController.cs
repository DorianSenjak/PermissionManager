using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Context;
using WebApplication1.ViewModels;
using System.Linq;

public class AppBazaOkolinaController : Controller
{
    private readonly MyDbContext _context;

    public AppBazaOkolinaController(MyDbContext context)
    {
        _context = context;
    }

    // Index method
    public IActionResult Index()
    {
        var entries = _context.App_Baza_Okolinas
            .Select(x => new AppBazaOkolinaVM
            {
                IDApp = (int)x.IDApp,
                AppName = _context.Apps.Where(a => a.AppID == x.IDApp).Select(a => a.AppName).FirstOrDefault(),
                IDBaza = (int)x.IDBaza,
                DatabaseName = _context.Bazas.Where(b => b.BazaID == x.IDBaza).Select(b => b.BazaName).FirstOrDefault(),
                IDOKL = (int)x.IDOKL,
                OkolinaName = _context.Okolinas.Where(o => o.OKLID == x.IDOKL).Select(o => o.OKLName).FirstOrDefault()
            })
            .ToList();

        return View(entries);
    }

    // GET: Edit
    [HttpGet]
    public IActionResult Edit(int appId, int databaseId, int OKLID)
    {
        var entry = _context.App_Baza_Okolinas
            .FirstOrDefault(x => x.IDApp == appId && x.IDBaza == databaseId && x.IDOKL == OKLID);

        if (entry == null)
        {
            return NotFound();
        }

        var model = new AppBazaOkolinaVM
        {
            IDApp = (int)entry.IDApp,
            IDBaza = (int)entry.IDBaza,
            IDOKL = (int)entry.IDOKL,
            AppName = _context.Apps.Where(a => a.AppID == entry.IDApp).Select(a => a.AppName).FirstOrDefault(),
            DatabaseName = _context.Bazas.Where(b => b.BazaID == entry.IDBaza).Select(b => b.BazaName).FirstOrDefault(),
            OkolinaName = _context.Okolinas.Where(o => o.OKLID == entry.IDOKL).Select(o => o.OKLName).FirstOrDefault()
        };

        // Populate dropdowns
        ViewBag.Apps = new SelectList(_context.Apps, "AppID", "AppName", model.IDApp);
        ViewBag.Databases = new SelectList(_context.Bazas, "BazaID", "BazaName", model.IDBaza);
        ViewBag.Okolinas = new SelectList(_context.Okolinas, "OKLID", "OKLName", model.IDOKL);

        return View(model);
    }

    // POST: Edit
    [HttpPost]
    public IActionResult Edit(AppBazaOkolinaVM model)
    {
        var entry = _context.App_Baza_Okolinas
            .FirstOrDefault(x => x.IDApp == model.IDApp && x.IDBaza == model.IDBaza && x.IDOKL == model.IDOKL);

        if (entry == null)
        {
            return NotFound();
        }

        entry.IDApp = model.IDApp;
        entry.IDBaza = model.IDBaza;
        entry.IDOKL = model.IDOKL;

        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    // GET: Delete
    [HttpGet]
    public IActionResult Delete(int appId, int databaseId, int OKLID)
    {
        var entry = _context.App_Baza_Okolinas
            .FirstOrDefault(x => x.IDApp == appId && x.IDBaza == databaseId && x.IDOKL == OKLID);

        if (entry == null)
        {
            return NotFound();
        }

        var model = new AppBazaOkolinaVM
        {
            IDApp = (int)entry.IDApp,
            AppName = _context.Apps.Where(a => a.AppID == entry.IDApp).Select(a => a.AppName).FirstOrDefault(),
            IDBaza = (int)entry.IDBaza,
            DatabaseName = _context.Bazas.Where(b => b.BazaID == entry.IDBaza).Select(b => b.BazaName).FirstOrDefault(),
            IDOKL = (int)entry.IDOKL,
            OkolinaName = _context.Okolinas.Where(o => o.OKLID == entry.IDOKL).Select(o => o.OKLName).FirstOrDefault()
        };

        return View(model);
    }

    // POST: Delete
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int appId, int databaseId, int OKLID)
    {
        var entry = _context.App_Baza_Okolinas
            .FirstOrDefault(x => x.IDApp == appId && x.IDBaza == databaseId && x.IDOKL == OKLID);

        if (entry == null)
        {
            return NotFound();
        }

        _context.App_Baza_Okolinas.Remove(entry);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}
