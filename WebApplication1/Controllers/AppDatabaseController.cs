using Microsoft.AspNetCore.Mvc;
using WebApplication1.Context;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using System.Linq;
using WebApplication1.Migrations;
using Microsoft.AspNetCore.Mvc.Rendering;

public class AppDatabaseController : Controller
{
    private readonly MyDbContext _context;

    public AppDatabaseController(MyDbContext context)
    {
        _context = context;
    }

    // Index method
    public IActionResult Index()
    {
        var appDatabases = _context.App_Bazas
            .Select(appBaza => new App_BazaVM
            {
                IDApp = (int)appBaza.IDApp,
                AppName = _context.Apps
                    .Where(a => a.AppID == appBaza.IDApp)
                    .Select(a => a.AppName)
                    .FirstOrDefault(), // Fetch the AppName

                IDBaza = (int)appBaza.IDBaza,
                DatabaseName = _context.Bazas
                    .Where(b => b.BazaID == appBaza.IDBaza)
                    .Select(b => b.BazaName)
                    .FirstOrDefault(), // Fetch the DatabaseName

                Username = appBaza.Username
            })
            .ToList();

        return View(appDatabases); // Pass the ViewModel to the view
    }

    // Create method (GET)
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // Create method (POST)
    [HttpPost]
    public IActionResult Create(App_BazaVM model)
    {
        if (ModelState.IsValid)
        {
            var appDatabase = new App_Baza
            {
                IDApp = model.IDApp,
                IDBaza = model.IDBaza,
                Username = model.Username
            };

            _context.App_Bazas.Add(appDatabase);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        return View(model);
    }

    // Edit method (GET)
    [HttpGet]
    public IActionResult Edit(int appId, int databaseId)
    {
        var appDatabase = _context.App_Bazas
            .FirstOrDefault(ad => ad.IDApp == appId && ad.IDBaza == databaseId);

        if (appDatabase == null)
        {
            return NotFound();
        }

        // Create the ViewModel
        var viewModel = new App_BazaVM
        {
            IDApp = (int)appDatabase.IDApp,
            AppName = _context.Apps
                .Where(a => a.AppID == appDatabase.IDApp)
                .Select(a => a.AppName)
                .FirstOrDefault(),
            IDBaza = (int)appDatabase.IDBaza,
            DatabaseName = _context.Bazas
                .Where(b => b.BazaID == appDatabase.IDBaza)
                .Select(b => b.BazaName)
                .FirstOrDefault(),
            Username = appDatabase.Username
        };

        // Populate the ViewBag with dropdown lists for Apps and Databases
        ViewBag.Apps = _context.Apps
            .Select(a => new SelectListItem
            {
                Text = a.AppName,
                Value = a.AppID.ToString(), // Store the AppID as the value
                Selected = a.AppID == appId // Mark the selected App
            })
            .ToList();

        ViewBag.Databases = _context.Bazas
            .Select(b => new SelectListItem
            {
                Text = b.BazaName,
                Value = b.BazaID.ToString(), // Store the BazaID as the value
                Selected = b.BazaID == databaseId // Mark the selected Database
            })
            .ToList();

        return View(viewModel);
    }



    // Edit method (POST)
    [HttpPost]
    public IActionResult Edit(App_BazaVM model)
    {
        if (ModelState.IsValid)
        {
            // Fetch the original entry in the App_Bazas table
            var existingAppDatabase = _context.App_Bazas
                .FirstOrDefault(ad => ad.IDApp == model.IDApp && ad.IDBaza == model.IDBaza);

            if (existingAppDatabase != null)
            {
                // Remove the existing entry
                _context.App_Bazas.Remove(existingAppDatabase);
            }

            // Create a new entry with the updated values
            var newAppDatabase = new App_Baza
            {
                IDApp = _context.Apps
                    .Where(a => a.AppName == model.AppName)
                    .Select(a => a.AppID)
                    .FirstOrDefault(), // Fetch AppID by AppName

                IDBaza = model.IDBaza, // This will remain the same
                Username = model.Username // Updated Username
            };

            // Add the new entry to the context
            _context.App_Bazas.Add(newAppDatabase);
            _context.SaveChanges(); // Save changes to the database

            return RedirectToAction("Index");
        }

        // If validation fails, repopulate the dropdown lists
        ViewBag.Apps = _context.Apps
            .Select(a => new SelectListItem
            {
                Text = a.AppName,
                Value = a.AppName // Use AppName for the dropdown
            })
            .ToList();

        ViewBag.Databases = _context.Bazas
            .Select(b => new SelectListItem
            {
                Text = b.BazaName,
                Value = b.BazaID.ToString()
            })
            .ToList();

        return View(model);
    }




    // Details method (GET)
    public IActionResult Details(int appId, int databaseId)
    {
        var appDatabase = _context.App_Bazas
            .FirstOrDefault(ad => ad.IDApp == appId && ad.IDBaza == databaseId);

        if (appDatabase == null)
        {
            return NotFound();
        }

        var viewModel = new App_BazaVM
        {
            IDApp = (int)appDatabase.IDApp,
            AppName = _context.Apps
                .Where(a => a.AppID == appDatabase.IDApp)
                .Select(a => a.AppName)
                .FirstOrDefault(),
            IDBaza = (int)appDatabase.IDBaza,
            DatabaseName = _context.Bazas
                .Where(b => b.BazaID == appDatabase.IDBaza)
                .Select(b => b.BazaName)
                .FirstOrDefault(),
            Username = appDatabase.Username
        };

        return View(viewModel);
    }

    // Controller: AppDatabaseController

    [HttpGet("AppDatabase/Delete/{appId}/{databaseId}")]
    public IActionResult Delete(int appId, int databaseId)
    {
        var appDatabase = _context.App_Bazas
            .FirstOrDefault(ad => ad.IDApp == appId && ad.IDBaza == databaseId);

        if (appDatabase == null)
        {
            return NotFound();
        }

        var viewModel = new App_BazaVM
        {
            IDApp = (int)appDatabase.IDApp,
            AppName = _context.Apps.FirstOrDefault(a => a.AppID == appId)?.AppName,
            IDBaza = (int)appDatabase.IDBaza,
            DatabaseName = _context.Bazas.FirstOrDefault(b => b.BazaID == databaseId)?.BazaName,
            Username = appDatabase.Username
        };

        return View(viewModel); // Pass the ViewModel to the view
    }

    [HttpPost("AppDatabase/Delete/{appId}/{databaseId}"), ActionName("Delete")]
    public IActionResult DeleteConfirmed(int appId, int databaseId)
    {
        var appDatabase = _context.App_Bazas
            .FirstOrDefault(ad => ad.IDApp == appId && ad.IDBaza == databaseId);

        if (appDatabase == null)
        {
            return NotFound();
        }

        _context.App_Bazas.Remove(appDatabase);
        _context.SaveChanges();

        // Explicitly redirect to the Index action of the AppDatabase controller
        return RedirectToAction("Index", "AppDatabase");
    }


}
